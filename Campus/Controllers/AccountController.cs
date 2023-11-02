using Campus.Common;
using Campus.Infrastructure;
using Campus.Models;
using Campus.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace Campus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 将数据从RegisterViewModel 复制到 IdentityUser
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                // 将用户数据存储在AspNetUsers数据库中
                var result = await _userManager.CreateAsync(user, model.Password);
                // 如果成功创建用户，则使用登陆服务登陆用户信息
                // 并重定向到HomeController的Index操作
                if (result.Succeeded)
                {
                    // 生成邮箱验证token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // 生成链接
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    // 记录生成的url
                    _logger.Log(LogLevel.Warning, confirmationLink);
                    var homeLink = Url.Action("index", "home", null, Request.Scheme);
                    #region 邮件发送
                    string subject = "账号激活";
                    string body = $"{user.UserName}.\n您最近使用此电子邮件地址注册了Campus。请通过以下链接完成账号激活：<a href=\"{confirmationLink}\">{confirmationLink}</a>\n如果您最近没有注册，或者认为您错误地收到了此电子邮件，请忽略此消息。\n谢谢！\nCampus团队只是为了让您知道，如果您最近没有注册，您收到此消息意味着其他人在尝试在Campus上创建帐户时输入了您的电子邮件地址。如果要为自己创建 Campus 帐户，请转到：<a href=\"{homeLink}\">{homeLink}</a>";
                    EmailUtil.Send(subject, body, user.Email);
                    #endregion

                    ViewBag.ErrorTitle = "注册成功！";
                    ViewBag.ErrorMessage = $"在您登陆之前，我们已经给您发了一封电子邮件，需要您先进行邮箱验证，单击邮箱内确认链接即可完成验证 <a href=\"{homeLink}\">{"我已验证，回到登录"}</a>";
                    return View("Error");
                }
                // 如果有任何错误，则将它们添加到ModelState对象中
                // 将由验证摘要标记助手显示到视图中
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("index", "home");
                }
                if (result.IsNotAllowed)
                    ModelState.AddModelError(String.Empty, "请先在邮箱激活您的帐号。");
                else
                    ModelState.AddModelError(String.Empty, "账号或密码有误。");

            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Json(true);
            return Json($"邮箱:{email}已经被使用了。");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return RedirectToAction("index", "home");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorTitle = "NotFound";
                ViewBag.ErrorMessage = $"用户{userId}未找到";
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return View();
            ViewBag.ErrorTitle = "Error";
            ViewBag.ErrorMessage = "链接已失效";
            return View("Error");
        }

        [HttpGet]
        public IActionResult ActivateUserEmail() => View();

        [HttpPost]
        public async Task<IActionResult> ActivateUserEmail(EmailAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 通过邮箱查询用户
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
                {
                    // 生成令牌
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // 生成链接
                    var confirmationLink = Url.Action("ConfirmEmail", "account", new { userId = user.Id, token = token }, Request.Scheme);
                    _logger.Log(LogLevel.Warning, confirmationLink);
                    #region 邮件发送
                    string subject = "账号激活";
                    string body = $"{user.UserName}.\n您最近使用此电子邮件地址注册了Campus。请通过以下链接完成账号激活：<a href=\"{confirmationLink}\">{confirmationLink}</a>\n如果您最近没有注册，或者认为您错误地收到了此电子邮件，请忽略此消息。\n谢谢！\nCampus团队只是为了让您知道，如果您最近没有注册，您收到此消息意味着其他人在尝试在Campus上创建帐户时输入了您的电子邮件地址。如果要为自己创建 Campus 帐户，请转到：<a href=\"{Url.Action("index", "home", null, Request.Scheme)}\">{Url.Action("index", "home", null, Request.Scheme)}</a>";
                    EmailUtil.Send(subject, body, user.Email);
                    #endregion
                    ViewBag.Message = $"我们已重新发送激活链接至邮箱{model.Email},请前往激活您的账户。";
                    return View("AcitvateUserEmailConfirmation");
                }
            }
            ViewBag.Message = "请确认邮箱是否正常，现在我们无法给您发送邮件。";
            // 为了避免被枚举或暴力攻击，不进行更多提示
            return View("AcitvateUserEmailConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(EmailAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"我们已重新发送了邮件至邮箱{model.Email},请前往邮箱重置您的密码。";
                //根据邮箱查询用户
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // 生成重置密码token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    // 生成链接
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);
                    // 将密码重置链接记录到日志
                    _logger.Log(LogLevel.Warning, passwordResetLink);
                    #region 邮件发送
                    string subject = "密码重置";
                    string body = $"{user.UserName}，您好：\n您收到这封电子邮件是因为您(也可能是某人冒充您的名义) 申请了一个重置密码的请求。\n假如这不是您本人所申请, 或者您曾持续收到这类的信件骚扰, 请您尽快联络管理员。\n您可以点击如下链接重新设置您的密码,如果点击无效，请把下面的代码拷贝到浏览器的地址栏中：\n<a href=\"{passwordResetLink}\">{passwordResetLink}</a>\n在访问链接之后, 您可以重新设置新的密码。\n如果您还有任何的疑问, 请与我们联系,更多帮助信息请点击: <a href=\"{Url.Action("index", "home", null, Request.Scheme)}\">{Url.Action("index", "home", null, Request.Scheme)}</a>。\n请不要直接回复本邮件。";
                    EmailUtil.Send(subject, body, user.Email);
                    #endregion
                    // 重定向到忘记密码确认视图
                    return View("ForgotPasswordConfirmation");
                }
                // 避免暴力或枚举，不进行更多提示
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (email == null || token == null)
            {
                ModelState.AddModelError(string.Empty, "链接已失效");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //通过邮箱获取用户
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //重置用户密码
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        #region 邮件发送
                        string subject = "帐号密码重置通知";
                        string body = $"Hi, {user.UserName}\n您的帐号密码已成功修改，请使用新密码<a href=\"{Url.Action("login", "account", null, Request.Scheme)}\">登录</a>使用。\nTips：\n如非本人操作，请及时检查帐号邮箱绑定并修改密码。\n修改密码请点此： {Url.Action("ForgotPassword", "Account", null, Request.Scheme)}\n（如点击无反应，请复制链接至浏览器中打开。）";
                        EmailUtil.Send(subject, body, user.Email);
                        #endregion
                        return View("ResetPasswordConfirmation");
                    }
                    // 显示错误信息。密码重置令牌已经使用或密码复杂性不符合标准
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied() {
            return View();
        }
    }
}

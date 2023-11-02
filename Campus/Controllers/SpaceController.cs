using Campus.Infrastructure;
using Campus.Models;
using Campus.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Campus.Controllers
{
    public class SpaceController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly CampusDbContext _campusDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SpaceController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            CampusDbContext campusDbContext,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _RoleManager = roleManager;
            _campusDbContext = campusDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{controller}")]
        public async Task<IActionResult> SpaceBase()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await _userManager.GetUserAsync(User);
                return RedirectToAction("SpaceBase", new { id = user.Id });
            }
            return RedirectToAction("login", "account");
        }

        [HttpGet("{controller}/{id}")]
        public async Task<IActionResult> SpaceBase(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var model = new SpaceViewModel
            {
                HeadPortrait = user.HeadPortrait,
                Nickname = user.Nickname,
                PersonalSignature = user.PersonalSignature,
                Gender = user.Gender.GenderValue,
                FollowsCount = user.Follows.Count,
                TargetsCount = user.Targets.Count
            };
            var roleList = await _userManager.GetRolesAsync(user);
            if (roleList == null || roleList.Count == 0)
            {
                model.Role = "游客";
            }
            else
            {
                model.Role = roleList[0];
            }
            ViewBag.Title = user.Nickname + "的个人空间";
            ViewBag.Id = id;
            return View(model);
        }

        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> MyPosts(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Redirect("error/404");
            return View(user.Works
                .Where(w => w.IsDelete == false)
                .OrderByDescending(w => w.ReleaseTime)
                .Select(w => new
                {
                    w.WorksId,
                    w.Title,
                    FabulousCount = w.Fabulous.Count,
                    CollectionsCount = w.Collections.Count,
                    w.Browse,
                    ReleaseTime = w.ReleaseTime.ToString("yyyy年MM月dd日HH时mm分")
                })
                .ToList());
        }

        [Authorize]
        [HttpGet("{controller}/setting/{action}")]
        public async Task<IActionResult> Info()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new SpaceSettingInfoViewModel
            {
                Birth = user.Birth,
                Gender = user.GenderId,
                Nickname = user.Nickname!,
                PersonalSignature = user.PersonalSignature!,
                UserName = user.UserName
            };
            ViewBag.Title = "我的信息";
            return View(model);
        }

        [Authorize]
        [HttpPost("{controller}/setting/{action}")]
        public async Task<IActionResult> Info(SpaceSettingInfoViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.Nickname = model.Nickname;
            user.PersonalSignature = model.PersonalSignature;
            user.GenderId = model.Gender;
            user.Birth = model.Birth;
            _campusDbContext.Update<AppUser>(user);
            _campusDbContext.SaveChanges();
            return RedirectToAction("SpaceBase");
        }

        [Authorize]
        [HttpGet("{controller}/setting/{action}")]
        public async Task<IActionResult> Face()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new SpaceSettingFaceViewModel
            {
                HeadPortrait = user.HeadPortrait!
            };
            ViewBag.Title = "我的头像";
            return View(model);
        }

        [Authorize]
        [Route("{controller}/setting/{action}")]
        [HttpPost]
        public async Task<IActionResult> Face(SpaceSettingFaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                // 判断文件类型
                string fileType = model.File.FileName.Substring(model.File.FileName.LastIndexOf(".")).ToLower();
                if (fileType == ".jpg" || fileType == ".png" || fileType == ".gif")
                {
                    if (model.File.Length > (1024 * 1024 * 2))
                    {
                        ModelState.AddModelError(string.Empty, "图片需小于2M");
                        return View(model);
                    }
                    // 将图片上传到wwwroot的images文件夹中
                    // 获取wwwroot的路径
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (System.IO.Directory.Exists(uploadFolder) == false)//如果不存在就创建file文件夹
                    {
                        System.IO.Directory.CreateDirectory(uploadFolder);
                    }
                    // 确保文件名字唯一
                    uniqueFileName = Guid.NewGuid() + fileType;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    // 使用IFormFile接口的CopyTo()方法
                    model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "文件类型只支持JPG、PNG、GIF格式");
                    return View(model);
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.HeadPortrait = "/images/" + uniqueFileName;
                _campusDbContext.Update(user);
                _campusDbContext.SaveChanges();
                return RedirectToAction("SpaceBase");
            }
            return View(model);
        }

        [Authorize]
        [HttpGet("{controller}/setting/{action}")]
        public async Task<IActionResult> Account()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new SpaceSettingFaceViewModel
            {
                HeadPortrait = user.HeadPortrait!
            };
            ViewBag.Title = "我的头像";
            return View(model);
        }
        [Authorize]
        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> FollowList(string id)
        {
            ViewBag.title = "全部关注";
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View("FollowList", user.Follows.Select((u) => new
            {
                Id = u.TargetId,
                PersonalSignature = u.Target.PersonalSignature,
                HeadPortrait = u.Target.HeadPortrait,
                Nickname = u.Target.Nickname
            }).ToList());
        }
        [Authorize]
        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> FollowListBtn(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var follow = _campusDbContext.Follows
            .FirstOrDefault(f => f.UserId == user.Id && f.TargetId == id);
            if (follow == null)
            {
                _campusDbContext.Follows.Add(new Follow
                {
                    TargetId = id,
                    UserId = user.Id
                });
            }
            else
            {
                _campusDbContext.Follows.Remove(follow);
            }
            _campusDbContext.SaveChanges();
            return Content("true");
        }
        [Authorize]
        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> FansList(string id)
        {
            ViewBag.title = "全部粉丝";
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View("FollowList", user.Targets.Select((u) => new
            {
                Id = u.UserId,
                PersonalSignature = u.User.PersonalSignature,
                HeadPortrait = u.User.HeadPortrait,
                Nickname = u.User.Nickname
            }).ToList());
        }
        [Authorize]
        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> ComplainsList()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = _campusDbContext.Opinions.Where(o => o.UserId == user.Id).ToList();
            return View(model);
        }
        [Authorize]
        [HttpGet("{controller}/{action}")]
        public async Task<IActionResult> Opinion(int id)
        {
            var model = await _campusDbContext.Opinions.FirstOrDefaultAsync(o => o.OpinionId == id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
    }
}

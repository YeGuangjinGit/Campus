using Campus.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Campus.Models;

using X.PagedList;
using Campus.Infrastructure;

namespace Campus.Controllers
{
    // [Authorize(Roles = "管理员")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly CampusDbContext _campusDbContext;

        public AdminController(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            CampusDbContext campusDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _campusDbContext = campusDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers(int? page, string searchValue = "")
        {
            int pageNumber = page ?? 1;
            int pageSize = 15;
            IPagedList<AppUser> users = await _userManager.Users
                .Where(u => u.UserName.Contains(searchValue) || u.Email.Contains(searchValue) || u.Nickname.Contains(searchValue))
                .OrderByDescending(u => u.CreateAt)
                .ToPagedListAsync(pageNumber, pageSize);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFpund");
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUser user)
        {
            var model = await _campusDbContext.Users.FindAsync(user.Id);
            if (model == null)
                return View("NotFound");
            model.Nickname = user.Nickname;
            model.HeadPortrait = user.HeadPortrait;
            model.Birth = user.Birth;
            model.PersonalSignature = user.PersonalSignature;
            _campusDbContext.Users.Update(model);
            await _campusDbContext.SaveChangesAsync();
            return View("EditUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> ListWorks(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 20;
            IPagedList<Works> works = await _campusDbContext.Works
                .Where(w => w.IsDelete == false)
                .OrderByDescending(w => w.ReleaseTime)
                .ToPagedListAsync(pageNumber, pageSize);
            return View(works);
        }

        [HttpGet]
        public async Task<IActionResult> EditWorks(int id)
        {
            var works = await _campusDbContext.Works.FindAsync(id);
            if (works == null)
            {
                ViewBag.ErrorMessage = "文章不存在！";
                return View("NotFound");
            }
            return View(works);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorks(Works model)
        {
            var works = await _campusDbContext.Works.FindAsync(model.WorksId);
            if (works == null)
                return View("NotFound");
            works.IsDelete = true;
            await _campusDbContext.SaveChangesAsync();
            return RedirectToAction("ListWorks");
        }

        [HttpGet]
        public async Task<IActionResult> ListSpecialColumns(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 20;
            IPagedList<SpecialColumn> specialColumns = await _campusDbContext.SpecialColumns
                .ToPagedListAsync(pageNumber, pageSize);
            return View(specialColumns);
        }

        [HttpGet]
        public IActionResult CreateSpecialColumn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialColumn(CreateSpecialColumnViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialColumn = new SpecialColumn
                {
                    SpecialColumnValue = model.SpecialColumnValue
                };
                await _campusDbContext.SpecialColumns.AddAsync(specialColumn);
                await _campusDbContext.SaveChangesAsync();
                return RedirectToAction("ListSpecialColumns");
            }
            return RedirectToAction("CreateSpecialColumn");
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 一个不重复的角色名来创建角色
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                // 将角色保存
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id={id}的信息不存在,请重试。";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name
            };
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                // 如果用户拥有角色，将用户名添加到 role 的users 集合中
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id={model.Id}的信息不存在,请重试。";
                return View("NotFound");
            }
            role.Name = model.RoleName;
            // 使用UpdateAsync()更新角色
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            // 获取角色
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id={role}的信息不存在,请重试。";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                // 判断当前用户是否在角色中
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);

                userRoleViewModel.IsSelected = isInRole;

                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"角色Id={roleId}的信息不存在,请重试。";
                return View("NotFound");
            }
            foreach (var userRole in model)
            {
                var user = await _userManager.FindByIdAsync(userRole.UserId);
                // 如果用户被选中了并且不在角色列表中 添加
                if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRole.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}

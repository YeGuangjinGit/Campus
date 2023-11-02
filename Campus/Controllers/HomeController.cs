using System.Reflection;
using Campus.Infrastructure;
using Campus.Models;
using Campus.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Campus.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly CampusDbContext _campusDbContext;

        public HomeController(UserManager<AppUser> userManager, CampusDbContext campusDbContext)
        {
            _userManager = userManager;
            _campusDbContext = campusDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new IndexViewModel();
            if (user != null)
            {
                model.HeadPortrait = user.HeadPortrait;
                model.IsAdmin = await _userManager.IsInRoleAsync(user, "管理员");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CampusWorld(string specialColumnValue)
        {
            var model = new CampusWorldViewModel();
            model.specialColumns = await _campusDbContext.SpecialColumns.OrderByDescending(s => s.Works.Count).ToListAsync();
            if (string.IsNullOrEmpty(specialColumnValue))
            {
                model.Works = await _campusDbContext.Works
                    .Where(w => w.IsDelete == false)
                    .OrderByDescending(w => w.ReleaseTime)
                    .ToListAsync();
            }
            else
            {
                if (await _campusDbContext.SpecialColumns.FirstOrDefaultAsync(s => s.SpecialColumnValue == specialColumnValue) == null)
                {
                    //ViewBag.ErrorMessage("专栏不存在");
                    return View("NotFound");
                }
                model.Works = await _campusDbContext.Works
                        .Where(w => w.IsDelete == false && w.SpecialColumn.SpecialColumnValue == specialColumnValue)
                        .OrderByDescending(w => w.ReleaseTime)
                        .ToListAsync();
            }
            ViewBag.Tip = specialColumnValue ?? "全部帖子";
            var path = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            model.Works.ForEach(w =>
            {
                w.Content = System.IO.File.ReadAllText(Path.Combine(path, w.Content));
                w.DisplayContent = System.IO.File.ReadAllText(Path.Combine(path, w.DisplayContent));
            });
            model.Top10Works = await model.Works
                .OrderByDescending(w => w.Browse)
                .Take(10)
                .ToListAsync();
            return View(model);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> AddComplains(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var works = _campusDbContext.Works.FirstOrDefault(w => w.WorksId == id);
            if (works == null) { return View("NotFound"); }
            var model = new Opinion
            {
                OpinionTitle = works.Title,
                UserId = user.Id
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddComplains(Opinion op)
        {
            _campusDbContext.Opinions.Add(op);
            await _campusDbContext.SaveChangesAsync();

            return RedirectToAction("CampusWorld", "Home");
        }
    }
}
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Campus.ViewModels;
using Campus.Models;
using Campus.Infrastructure;
using Campus.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Campus.CustomerMiddlewares;

namespace Campus.Controllers
{
    [Authorize]
    public class PlatformController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CampusDbContext _campusDbContext;
        // 提供数据的加密和解密
        private readonly IDataProtector _dataProtector;

        public PlatformController(UserManager<AppUser> userManager,
            CampusDbContext campusDbContext,
            IWebHostEnvironment webHostEnvironment,
            IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _userManager = userManager;
            _campusDbContext = campusDbContext;
            _webHostEnvironment = webHostEnvironment;
            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.WorksIdRouteValue);
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            PlatformUploadViewModel model = new()
            {
                HeadPortrait = user.HeadPortrait,
                SpecialColumns = _campusDbContext.SpecialColumns.ToList()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(PlatformUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var now = DateTime.Now;
                string startPath = $"{Directory.GetParent(Assembly.GetExecutingAssembly().Location)}";
                var intermediatePath = Path.Combine("files", now.ToString("yyyy-MM-dd")); 
                var physicalPath = Path.Combine(startPath, intermediatePath); 
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
                string fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}.campusori";
                string displayFileName = $"{fileName}.disp";
                await System.IO.File.WriteAllTextAsync(Path.Combine(startPath, intermediatePath, fileName), model.Content);
                await System.IO.File.WriteAllTextAsync(Path.Combine(startPath, intermediatePath, displayFileName), model.Content.NoHTML());
                Works works = new()
                {
                    UserId = user.Id,
                    Title = model.Title,
                    Content = Path.Combine(intermediatePath, fileName),
                    DisplayContent = Path.Combine(intermediatePath, displayFileName),
                    ReleaseTime = DateTime.Now,
                    SpecialColumnId = model.SpecialColumnId
                };
                _campusDbContext.Works.Add(works);
                _campusDbContext.SaveChanges();
                return RedirectToAction("Read", new { id = works.WorksId });
            }
            model.SpecialColumns = _campusDbContext.SpecialColumns.ToList();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            if (file.Length > (1024 * 1024 * 2)) return Content("/img/bigImg.png");
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (System.IO.Directory.Exists(uploadFolder) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(uploadFolder);
            }
            string fileType = file.FileName.Substring(file.FileName.LastIndexOf("."));
            // 确保文件名字唯一
            string uniqueFileName = Guid.NewGuid() + fileType;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return Content("/images/" + uniqueFileName);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Read(string id)
        {
            int worksId = -1;
            if (!int.TryParse(id, out worksId))
            {
                worksId = Convert.ToInt32(_dataProtector.Unprotect(id));
            }


            var works = await _campusDbContext.Works
                .Where(w => w.IsDelete == false)
                .FirstOrDefaultAsync(w => w.WorksId == worksId);

            if (works == null) return NotFound();
            works.Browse += 1;
            await _campusDbContext.SaveChangesAsync();
            var model = new PlatformReadViewModel()
            {
                WorksId = worksId,
                Browse = works.Browse,
                Content = await System.IO.File.ReadAllTextAsync(
                    Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString(), works.Content)),
                Comments = works.Comments.OrderByDescending(c => c.ReleaseTime),
                FabulousNum = works.Fabulous.Count,
                User = works.User,
                MyHeadPortrait = "",
                Nickname = works.User.Nickname,
                ReleaseTime = works.ReleaseTime,
                SpecialColumnValue = works.SpecialColumn.SpecialColumnValue,
                TargetsNum = works.User.Targets.Count,
                Title = works.Title
            };
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                model.MyHeadPortrait = user.HeadPortrait;
                model.FollowButton = user.Follows.FirstOrDefault(f => f.TargetId == works.User.Id) == null;
                model.CollectionButton = user.Collections.FirstOrDefault(f => f.WorksId == works.WorksId && f.UserId == user.Id) == null;
                model.GoodButton = user.Fabulous.FirstOrDefault(f => f.WorksId == works.WorksId && f.UserId == user.Id) == null;
            }
            return View(model);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> submitComment(int id, string text)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _campusDbContext.Comments.Add(new Comment
            {
                UserId = user.Id,
                WorksId = id,
                Content = text
            });
            _campusDbContext.SaveChanges();
            return RedirectToAction("Comments", new { id = id });
        }
        
        public async Task<IActionResult> submitComments(int id, string text2, int works)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _campusDbContext.Comments.Add(new Comment
            {
                UserId = user.Id,
                Parentid = id,
                Content = text2
            });
            _campusDbContext.SaveChanges();
            var comment = _campusDbContext.Comments.FirstOrDefault(c => c.CommentId == id);
            if (comment == null)
                return View("NotFoud");
            var model = new CommentViewModel
            {
                WorksId = works,
                Comments = comment.Comments.OrderByDescending(c => c.ReleaseTime),
                HeadPortrait = user.HeadPortrait!,
                Parentid = id
            };
            return View("second_comments", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Follow(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.Follows.FirstOrDefault(f => f.TargetId == id) != null)
            {
                return Content("false");
            }
            try
            {
                _campusDbContext.Follows.Add(new Follow
                {
                    UserId = user.Id,
                    TargetId = id
                });
                _campusDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return Content("true");
        }
        
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var works = user.Works.Where(w=>w.IsDelete==false).OrderByDescending(w => w.ReleaseTime);

            var model = new List<WorksManage>();
            foreach (var item in works)
            {
                var workManage = new WorksManage
                {
                    Id = item.WorksId,
                    EncryptedId = _dataProtector.Protect(item.WorksId.ToString()),
                    Browse = item.Browse,
                    CollectionCount = item.Collections.Count,
                    CommentCount = _campusDbContext.Comments.Where(c => c.WorksId == item.WorksId).Count(),
                    DisplayContent = item.DisplayContent.Length >= 50 ? item.DisplayContent[..50] + "..." : item.DisplayContent,
                    ReleaseTime = item.ReleaseTime,
                    Title = item.Title,
                    FabulouCount = item.Fabulous.Count,
                    SpecialColumnValue = item.SpecialColumn.SpecialColumnValue
                };

                int start = item.Content.IndexOf("<img src=");
                int end;
                if (start == -1)
                {
                    workManage.Img = "/img/nullImg.png";
                }
                else
                {
                    start += 10;
                    end = item.Content.IndexOf("\"", start);
                    workManage.Img = item.Content.Substring(start, end - start);
                }
                model.Add(workManage);
            }
            return View(model);
        }
        public async Task<IActionResult> Comments(int id)
        {
            var works = await _campusDbContext.Works.FirstOrDefaultAsync(w => w.WorksId == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (works == null)
                return View("NotFound");

            var model = new CommentViewModel
            {
                Comments = works.Comments.OrderByDescending(c => c.ReleaseTime).ToList(),
                HeadPortrait = user.HeadPortrait,
                WorksId = id
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMyWorks(string id)
        {
            string decryptedId = _dataProtector.Unprotect(id);
            int decryptedWorksId = Convert.ToInt32(decryptedId);
            var works = await _campusDbContext.Works.FirstOrDefaultAsync(w => w.WorksId == decryptedWorksId);
            if (works == null)
            {
                return View("NotFound");
            }
            works.IsDelete = true;
            _campusDbContext.SaveChanges();
            return RedirectToAction("Manage");
        }
        [Authorize]
        public async Task<IActionResult> SubmitCollection(int id)
        {
            bool flag = true;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var collect = await _campusDbContext.Collections.FirstOrDefaultAsync(c => c.UserId == user.Id && c.WorksId == id);
            if (collect == null)
            {
                _campusDbContext.Collections.Add(new Collection
                {
                    WorksId = id,
                    UserId = user.Id
                });
            }
            else
            {
                _campusDbContext.Collections.Remove(collect);
                flag = false;
            }
            _campusDbContext.SaveChanges();
            return Content(flag.ToString());
        }

        [Authorize]
        public async Task<IActionResult> SubmitFabulous(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var fabulou = await _campusDbContext.Fabulous.FirstOrDefaultAsync(c => c.UserId == user.Id && c.WorksId == id);
            if (fabulou == null)
            {
                _campusDbContext.Fabulous.Add(new Fabulous
                {
                    WorksId = id,
                    UserId = user.Id
                });
            }
            else
            {
                _campusDbContext.Fabulous.Remove(fabulou);
            }
            _campusDbContext.SaveChanges();
            return Content("true");
        }
        
        public async Task<IActionResult> CollectionView(string id, int work)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound");
            }
            var model = user.Collections.Where(c => c.Works.IsDelete == false).ToList();
            return View(model);
        }
        
        public async Task<IActionResult> CollectionBtn(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var collection = _campusDbContext.Collections
            .FirstOrDefault(f => f.UserId == user.Id && f.WorksId == id);
            if (collection == null)
            {
                _campusDbContext.Collections.Add(new Collection
                {
                    WorksId = id,
                    UserId = user.Id
                });
            }
            else
            {
                _campusDbContext.Collections.Remove(collection);
            }
            _campusDbContext.SaveChanges();
            return Content("true");
        }
        
        public IActionResult Submission_Guide() { 
            return View();
        }
    }
}

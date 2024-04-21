using App.Data;
using App.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebAppMVC_1.Models;
using AppMVC.Models.Blog;
using System.Collections.Generic;
using System;

namespace WebAppMVC_1.Areas.Database1.Controllers
{
    [Area("Database1")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<DbManageController> _logger;
        public DbManageController(AppDbContext context , RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ILogger<DbManageController> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { set; get; }
        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success =  await  _context.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa Database thành công" : "Xóa không thành công";
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public async Task<IActionResult> MigrationAsync()
        {
            await _context.Database.MigrateAsync(); // Cập nhật các Migration ở trạng thái Pending
            StatusMessage = "Cập nhập Migration thành công";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            _logger.LogInformation("Quang VInh ");
            foreach ( var r in roleNames)
            {
                var roleName = (string)r.GetRawConstantValue();
                _logger.LogInformation(roleName);
                var role =  await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var useradmin = await _userManager.FindByNameAsync("admin");
            if(useradmin == null)
            {
                useradmin = new AppUser
                {
                    UserName = "admin", 
                    EmailConfirmed = true,
                    Email = "admin@example.com"
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
            }

            SeedPostCategory();
            StatusMessage = "SeedData thành công";
            return RedirectToAction("Index");
        }

        private void SeedPostCategory()
        {
            _context.Blogs.RemoveRange(_context.Blogs.Where(c => c.Description.Contains("[Fakedata]")));
            _context.Posts.RemoveRange(_context.Posts.Where(p => p.Content.Contains("[Fakepost]")));
            // Code generate fake Category 
            var fakeCategory = new Faker<Category>();
            int cm = 1;
            fakeCategory.RuleFor(c => c.Title, f => $"CM{cm++}" + f.Lorem.Sentence(1, 2).Trim('.'));
            fakeCategory.RuleFor(c => c.Description, f => f.Lorem.Sentences(5) + "[Fakedata]");
            fakeCategory.RuleFor(c => c.Slug, f => f.Lorem.Slug(1));

            var cate1 = fakeCategory.Generate();
                 var cate11 = fakeCategory.Generate();
                 var cate12 = fakeCategory.Generate();
            var cate2 = fakeCategory.Generate();
                var cate21 = fakeCategory.Generate();
                    var cate221 = fakeCategory.Generate();

            cate11.ParentCategory = cate1;
            cate12.ParentCategory = cate1;
            cate21.ParentCategory = cate2;
            cate221.ParentCategory = cate21;

            var rangeCate = new Category[] {cate1, cate11,cate12,cate2,cate21,cate221 };
            _context.Blogs.AddRange(rangeCate);

            var rCateIndex = new Random();
            //Code generate fake Post and PostCategory
            int bv = 1;

            var fakerPost = new Faker<Posts>();
            var user = _userManager.GetUserAsync(this.User).Result;
            fakerPost.RuleFor(p => p.AuthorID, f => user.Id);
            fakerPost.RuleFor(p => p.Content, f => f.Lorem.Paragraph(7) + "[Fakepost]");
            fakerPost.RuleFor(p => p.CreatedDate, f => f.Date.Between(new System.DateTime(2021, 1, 1), new System.DateTime(2021, 7, 1)));
            fakerPost.RuleFor(p => p.Description, f => f.Lorem.Sentences(3));
            fakerPost.RuleFor(p => p.Published, f => true);
            fakerPost.RuleFor(p => p.Slug, f => f.Lorem.Slug());
            fakerPost.RuleFor(p => p.Title, f => $"Bài {bv++}" + f.Lorem.Sentence(3, 4).Trim('.'));

            List<Posts> posts = new List<Posts>();
            List<PostCategory> postCategories = new List<PostCategory>();
            for (int i=0; i< 40; i++)
            {
                var post = fakerPost.Generate();
                post.UpdatedDate = post.CreatedDate;
                posts.Add(post);
                postCategories.Add(new PostCategory()
                {
                    Posts = post,
                    Category = rangeCate[rCateIndex.Next(5)]
                });
            }
            _context.Posts.AddRange(posts);
            _context.PostCategory.AddRange(postCategories);
            _context.SaveChanges();
        }
    }
}

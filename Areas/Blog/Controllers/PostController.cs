using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMVC.Models.Blog;
using WebAppMVC_1.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;
using App.Areas.Identity.Models.UserViewModels;
using App.Models;
using AppMVC.Areas.Blog.Models;
using Microsoft.AspNetCore.Identity;
using App.Utilities;


namespace AppMVC.Areas_Blog_Controllers
{
    [Area("Blog")]
    [Route("admin/post/category/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        [TempData]
        public string StatusMessage { get; set; }   

        public PostController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index([FromQuery(Name ="p")]int currentPage, int pagesize =10)
        {
            var post = _context.Posts
                        .Include(p => p.AppUser)
                        .OrderByDescending(p => p.UpdatedDate);
                        
            int countPost = await post.CountAsync();
            int countPage = (int)Math.Ceiling((double)countPost / pagesize);

            if(currentPage > countPage) currentPage = countPage;
            if(currentPage <1 ) currentPage = 1;
            ViewBag.orderedPage = (currentPage-1)*pagesize;
            ViewBag.countPost = countPost;
            var pageModel = new PagingModel() {
                currentpage = currentPage,
                countpages = countPage,
                generateUrl = (pagenumber) => Url.Action("Index" , new { p = pagenumber, pagesize = pagesize })
            };
            ViewBag.pagingModel = pageModel;

            //model.totalUsers = await qr.CountAsync();
            //model.countPages = (int)Math.Ceiling((double)model.totalUsers / model.ITEMS_PER_PAGE);

            //if (model.currentPage < 1)
            //    model.currentPage = 1;
            //if (model.currentPage > model.countPages)
            //    model.currentPage = model.countPages;

            var postInPage  = await post.Skip((currentPage - 1) * pagesize)
                        .Take(pagesize)
                        .Include(p => p.PostCategories)
                        .ThenInclude(pc => pc.Category).ToListAsync();
            return View (postInPage);
        }
        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            //Create MultiSelectList object to pass into the View of Create\
            var category =   await _context.Blogs.ToListAsync();
            ViewData["Category"] = new MultiSelectList(category, "Id", "Title");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Slug,Content,Published,CategoriesId")] CreatePostModel posts)
        {
            var category =  await _context.Blogs.ToListAsync();
            ViewData["Category"] = new MultiSelectList(category, "Id", "Title");
            if(posts.Slug == null)
            {
                posts.Slug = AppUtilities.GenerateSlug(posts.Title);
            }
            var duplicatedSlud = await _context.Posts.AnyAsync(p => p.Slug == posts.Slug);
            if (duplicatedSlud)
            {
                ModelState.AddModelError(String.Empty, "Chuỗi định dạng URL không được phép trùng");
                return View(posts);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                posts.CreatedDate = posts.UpdatedDate = DateTime.Now;
                posts.AuthorID  = user.Id;
                _context.Add(posts);
                if(posts.CategoriesId != null)
                {
                    foreach ( var CateID in posts.CategoriesId)
                    {
                        _context.Add(new PostCategory()
                        {
                            CategoryId = CateID,
                            Posts = posts,
                        });
                    }
                }
                await _context.SaveChangesAsync();
                StatusMessage = "Thêm bài viết thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts =  await _context.Posts.Include(p => p.PostCategories).FirstOrDefaultAsync(p => p.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }
            var createPostModel = new CreatePostModel()
            {
                PostId = posts.PostId,
                Title = posts.Title,
                Description = posts.Description,
                Content = posts.Content,
                Published = posts.Published,
                Slug = posts.Slug,
                CategoriesId = posts.PostCategories.Select(pc => pc.CategoryId).ToArray(),
            };

            var category =   await _context.Blogs.ToListAsync();
            ViewData["Category"] = new MultiSelectList(category, "Id", "Title");
            return View(createPostModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Description,Slug,Content,Published,CategoriesId")] CreatePostModel posts)
        {
            if (id != posts.PostId)
            {
                return NotFound();
            }

            var categories =  await _context.Blogs.ToListAsync();
            ViewData["Category"] = new MultiSelectList(categories, "Id", "Title");

            if(posts.Slug == null)
            {
                posts.Slug = AppUtilities.GenerateSlug(posts.Title);
            }
            var duplicatedSlug =  await _context.Posts.AnyAsync(p => p.Slug == posts.Slug && p.PostId != id);
            if(duplicatedSlug)
            {
                ModelState.AddModelError(string.Empty, "Chuỗi định danh không được phép trùng");
                return View(posts);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //Lấy Post trước khi cập nhật => Giá trị gốc
                   var postUpdate =  _context.Posts.Include(p => p.PostCategories).FirstOrDefault(p => p.PostId == id);
                    if(postUpdate == null )
                    {
                        return NotFound();
                    }
                    //Gán các giá trị mới được Blinding vào Post gốc 
                    postUpdate.Title = posts.Title;
                    postUpdate.Slug = posts.Slug;
                    postUpdate.Description = posts.Description;
                    postUpdate.Content  = posts.Content;
                    postUpdate.Published = posts.Published;
                    postUpdate.UpdatedDate = DateTime.Now; 

                    //Gán lại giá trị CategoryId cho Post gốc 
                    if(posts.CategoriesId == null)
                    {
                        posts.CategoriesId = new int[] { }; 
                    }
                    var oldCateIds = postUpdate.PostCategories.Select(pc => pc.CategoryId).ToArray();
                    var newCateIds = posts.CategoriesId;

                    var deleteCatePost = from postCate in postUpdate.PostCategories
                                     where (!newCateIds.Contains(postCate.CategoryId))
                                     select postCate;
                    _context.PostCategory.RemoveRange(deleteCatePost);

                    var addCatePostIds = from cateid in newCateIds
                                         where !oldCateIds.Contains(cateid)
                                         select cateid;
                    foreach(var catepostId in addCatePostIds)
                    {
                        _context.PostCategory.Add(new PostCategory
                        {
                            CategoryId = catepostId,
                            PostId = id,
                        });
                    }

                    _context.Update(postUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", posts.AuthorID);
            return View(posts);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();
            StatusMessage = $"Đã xóa bài viết: {posts.Title} thành công ";
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}

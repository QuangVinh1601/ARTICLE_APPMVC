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

namespace AppMVC.Areas_Blog_Controllers
{
    [Area("Blog")]
    [Route("admin/blog/category/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var blog = _context.Blogs.Include(c => c.ParentCategory)
                                     .Include(c => c.CategoryChildren);
            var parentCategoryRoot =  (await blog.ToListAsync())
                                      .Where (c => c.ParentCategoryId == null); 
           return View(parentCategoryRoot);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Blogs
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public async Task<IActionResult> Create()
        {
            var categoryList = await (_context.Blogs
                                     .Include(c => c.ParentCategory)
                                     .Include(c => c.CategoryChildren)).ToListAsync();
            var categoryRoot = categoryList.Where(c => c.ParentCategoryId == null).ToList();
            categoryRoot.Insert(0, new Category()
            {
                Id = -1,
                Title = "Không có phần tử cha"
            });
            var listContainCategory = new List<Category>();
            var selectListCategory = SelectListCategory(categoryRoot, listContainCategory, 1);
            var selectList = new SelectList(selectListCategory, "Id", "Title");
            ViewData["ParentCategoryId"] = selectList;
            return View();
        }
        private List<Category> SelectListCategory(List<Category> categoryRoot, List<Category> listContainCategory, int space  )
        {
          

            foreach ( var cRoot in categoryRoot)
            {
                string prefix = String.Concat(Enumerable.Repeat("--", space));
                listContainCategory.Add(new Category() //Tạo đối tượng Category mới để không ảnh hưởng đến categoryRoot
                {
                    Id = cRoot.Id,
                    Title = prefix +  cRoot.Title,
                });
                if(cRoot.CategoryChildren?.Count>0)
                {
                    SelectListCategory(cRoot.CategoryChildren.ToList(), listContainCategory, space+1);
                }
            }
            return listContainCategory;
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Slug,ParentCategoryId")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ParentCategoryId == -1)
                {
                    category.ParentCategoryId = null;
                } 
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categoryList = await (_context.Blogs
                          .Include(c => c.ParentCategory)
                          .Include(c => c.CategoryChildren)).ToListAsync();
            var categoryRoot = categoryList.Where(c => c.ParentCategoryId == null).ToList();
            categoryRoot.Insert(0, new Category()
            {
                Id = -1,
                Title = "Không có phần tử cha"
            });
            var listContainCategory = new List<Category>();
            var selectListCategory = SelectListCategory(categoryRoot, listContainCategory, 2);
            var selectList = new SelectList(selectListCategory, "Id", "Title");
            ViewData["ParentCategoryId"] = selectList;
            return View();
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Blogs.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryList =  await (_context.Blogs
                               .Include(c => c.ParentCategory)
                               .Include(c => c.CategoryChildren)).ToListAsync();
            var categoryParent = categoryList.Where(c => c.ParentCategoryId == null).ToList();
            categoryParent.Insert(0, new Category()
            {
                Id = -1,
                Title = "Không có phần từ cha"
            });
            var listContainCategory = new List<Category>();
            List<Category> selectList =  SelectListCategory(categoryParent, listContainCategory , 2);
            var selectList1= new SelectList(selectList, "Id", "Title");
            ViewData["ParentCategoryId"] = selectList1;
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Slug,ParentCategoryId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid && category.ParentCategoryId != category.Id)
            {
                try
                {
                    if(category.ParentCategoryId == -1)
                    {
                        category.ParentCategoryId = null;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            var categoryList = await (_context.Blogs
                               .Include(category => category.ParentCategory)
                               .Include(category => category.CategoryChildren)).ToListAsync();
            var categoryRoot = categoryList.Where(c => c.ParentCategoryId == null).ToList();
            List<Category> categories = new List<Category>();
            List<Category> listContainCategory=  SelectListCategory(categoryRoot, categories, 1);
            ViewData["ParentCategoryID"] = new SelectList(listContainCategory, "Id", "Title");
            return View(category);
                               
            
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Blogs
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await (_context.Blogs
                           .Include(c => c.ParentCategory)
                           .Include(c => c.CategoryChildren)).ToListAsync();
            var categoryNeedDelete = category.Where(c => c.Id == id).FirstOrDefault();

            if(categoryNeedDelete == null)
            {
                return NotFound();
            }
            foreach (var cCategory in categoryNeedDelete.CategoryChildren)
            {
                cCategory.ParentCategoryId = categoryNeedDelete.ParentCategoryId;
            }            
            _context.Blogs.Remove(categoryNeedDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}

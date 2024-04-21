using AppMVC.Models.Blog;
using System.ComponentModel.DataAnnotations;
namespace AppMVC.Areas.Blog.Models
{
    public class CreatePostModel : Posts
    {
        [Display(Name ="Chuyên mục")]
        public int[] CategoriesId { get; set; }
    }
}

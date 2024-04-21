using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.Blog
{
    public class PostCategory
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("PostId")]
        public Posts Posts { get; set; }
    }
}

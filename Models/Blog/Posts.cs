using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.Blog
{
    [Table("Posts")]
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        [Required(ErrorMessage ="Phải có tiêu đề bài viết")]
        [Display(Name ="Tiêu đề bài viết")]
        [StringLength(160, MinimumLength = 5 , ErrorMessage = "{0} phải dài từ {1} đến {2}")]
        public string Title { get; set; }

        [Display(Name ="Mô tả ngắn")]
        public string Description { get; set; }

        [Display(Name ="Chuỗi định danh Url", Prompt ="Nhập hoặc để trống tự phát sinh theo Title")]
        [StringLength(160, MinimumLength =5 , ErrorMessage ="{0} phải dài từ {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage ="Chỉ dùng các ký tự [a-z0-9]")]

        public string Slug { get; set; }
        [Display(Name ="Nội dung")]
        public string Content { get; set; }

        [Display(Name ="Xuất bản")]
        public bool Published { set; get; }
        public ICollection<PostCategory> PostCategories { set; get; }

        // [Required(ErrorMessage ="Phải nhập tên tác giả")]
        [Display(Name ="Tác giả ")]
        public string AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        [Display(Name ="Tác giả")]        
        
        public AppUser AppUser { get; set; }
        [Display(Name ="Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        [Display(Name ="Ngày cập nhật")]
        public DateTime UpdatedDate { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppMVC_1.Models.Contact;
using Microsoft.AspNetCore.Identity;
using App.Models;
using AppMVC.Models.Blog;
namespace WebAppMVC_1.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<Category> Blogs { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<PostCategory> PostCategory { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }

            }
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Slug).IsUnique();
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(pc => new { pc.CategoryId, pc.PostId });
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasIndex(p => p.Slug).IsUnique();
            });


        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);    
        }
    }
}

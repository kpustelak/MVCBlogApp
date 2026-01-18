using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Models;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Db;

public class BlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.PostCategory)
            .WithMany(pc => pc.Posts) 
            .HasForeignKey(p => p.PostCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<PostCategory>();
        modelBuilder.Entity<PostImage>();
        base.OnModelCreating(modelBuilder);
    }
}
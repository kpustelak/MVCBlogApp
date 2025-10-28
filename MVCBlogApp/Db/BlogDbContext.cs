using Microsoft.EntityFrameworkCore;
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
}
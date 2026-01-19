using Microsoft.AspNetCore.Identity;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Db;

public class DbSeeder
{
    private readonly BlogDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DbSeeder(BlogDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void SeedData()
    {
        _context.Database.EnsureCreated();
        SeedCategories();
        SeedAdmin();
    }

    private void SeedCategories()
    {
        if (!_context.PostCategories.Any())
        {
            var categories = new List<PostCategory>
            {
                new PostCategory { Name = "Homelab", IconBootstrapLink = "bi-backpack" },
                new PostCategory { Name = "Wordpress", IconBootstrapLink = "bi-wordpress" },
                new PostCategory { Name = "Coding", IconBootstrapLink = "bi-code" }
            };
            
            _context.PostCategories.AddRange(categories);
            _context.SaveChanges(); 
        }
    }

    private void SeedAdmin()
    {
        var adminEmail = "admin@blog.pl";
        
        var existingUser = _userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
        
        if (existingUser == null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            _userManager.CreateAsync(admin, "Admin123!!!").GetAwaiter().GetResult();
        }
    }
}
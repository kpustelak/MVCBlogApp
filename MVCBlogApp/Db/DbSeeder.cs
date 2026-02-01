using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Db;

public class DbSeeder
{
    private readonly BlogDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(
        BlogDbContext context, 
        UserManager<IdentityUser> userManager,
        IConfiguration configuration,
        ILogger<DbSeeder> logger)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
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
            
            _logger.LogInformation("Seeded {Count} categories", categories.Count);
        }
    }

    private void SeedAdmin()
    {
        var adminEmail = _configuration["AdminUser:Email"] ?? "admin@blog.pl";
        var adminPassword = _configuration["AdminUser:Password"] ?? "BardzoSilneHaslo123!@#LongerThan20";
        
        var existingUser = _userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
        
        if (existingUser == null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = true
            };

            var result = _userManager.CreateAsync(admin, adminPassword).GetAwaiter().GetResult();
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Admin user created: {Email}", adminEmail);
            }
            else
            {
                _logger.LogError("Failed to create admin");
                foreach (var error in result.Errors)
                {
                    _logger.LogError("Error: {Error}", error.Description);
                }
            }
        }
        else
        {
            _logger.LogInformation("Admin already exists");
        }
    }
}
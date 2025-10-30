using MVCBlogApp.Models.Entities;
using PostCategory = MVCBlogApp.Models.Entities.PostCategory;

namespace MVCBlogApp.Db;

public class DbSeeder
{
    private readonly BlogDbContext _context;
    public DbSeeder(BlogDbContext context)
    {
        _context = context;
    }

    public void SeedCategories()
    {
        if (!_context.PostCategories.Any())
        {
            IEnumerable<PostCategory> categories = new List<PostCategory>()
            {
                new  PostCategory()
                {
                    Id = 1,
                    Name = "Homelab",
                    IconBootstrapLink = "bi-backpack"
                },
                new  PostCategory()
                {
                    Id = 2,
                    Name = "Wordpress",
                    IconBootstrapLink = "bi-wordpress"
                },
                new  PostCategory()
                {
                    Id = 3,
                    Name = "Coding",
                    IconBootstrapLink = "bi-code"
                }
            };
            _context.PostCategories.AddRange(categories);
            _context.SaveChanges();
        }
    }
}
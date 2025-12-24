using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Service;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    public PostService(BlogDbContext context)
    {
        _context = context;
    }
    public async Task<Post> GetWholePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        return post;
    }

    public async Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAsync(int pageNumber, int pageSize)
    {
        var posts = await _context.Posts
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ShortPostModelDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt,
                IsPublished = x.IsPublished
            })
            .ToListAsync();

        return posts;
    }
    
    public async Task<List<ShortPostModelDto>> GetListOfPostsAsync(int top, bool pickFavourite)
    {
        var posts = await _context.Posts
            .Where(x => x.IsPublished == true && x.IsFavourite == pickFavourite)
            .OrderByDescending(x => x.CreatedAt)
            .Take(top)
            .Select(x => new ShortPostModelDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt,
                IsPublished = x.IsPublished,
                Excerpt = x.Excerpt,
                FeaturedImage = x.FeaturedImage
            })
            .ToListAsync();
        return posts;
    }
}
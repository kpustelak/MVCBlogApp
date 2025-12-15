using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Service;

public class PostPublishingService : IPostPublishingService
{
    private readonly BlogDbContext _context;
    
    public PostPublishingService(BlogDbContext context)
    {
        _context = context;
    }
    
    public async Task<Post> AddPostAsync(AddOrEditPostDto addPostDto)
    {
        var post = new Post
        {
            Title = addPostDto.Title,
            Slug = addPostDto.Slug,
            Content = addPostDto.Content,
            PostCategoryId = addPostDto.PostCategoryId,
            IsPublished = addPostDto.IsPublished,
            CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            FeaturedImageUrl = addPostDto.FeaturedImageUrl
        };
        
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }
    
    public async Task DeletePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {postId} does not exist");
        }
        
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> GetWholePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {postId} not found");
        }
        
        return post;
    }
    
    public async Task<Post> EditPostAsync(AddOrEditPostDto newPostData, int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {postId} does not exist");
        }
        
        post.Title = newPostData.Title;
        post.Slug = newPostData.Slug;
        post.Content = newPostData.Content;
        post.IsPublished = newPostData.IsPublished;
        post.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        post.FeaturedImageUrl = newPostData.FeaturedImageUrl;
        
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<int> GetTotalPostCountAsync()
    {
        return await _context.Posts.CountAsync();
    }

    public async Task<List<Post>> GetPostDataListAsync(int page, int pageSize)
    {
        return await _context.Posts
            .OrderByDescending(x => x.CreatedAt) 
            .Skip((page - 1) * pageSize)          
            .Take(pageSize)
            .ToListAsync();           
    }
}
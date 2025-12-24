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
            Slug = addPostDto.Slug == null ? string.Empty : addPostDto.Slug,
            Content = addPostDto.Content == null ? string.Empty : addPostDto.Content,
            PostCategoryId = addPostDto.PostCategoryId,
            IsPublished = addPostDto.IsPublished,
            CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            FeaturedImageUrl = addPostDto.FeaturedImageUrl == null ? string.Empty : addPostDto.FeaturedImageUrl
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
        post.Slug = newPostData.Slug == null ? string.Empty : newPostData.Slug;
        post.Content = newPostData.Content == null ? string.Empty : newPostData.Content;
        post.IsPublished = newPostData.IsPublished;
        post.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        post.FeaturedImageUrl = newPostData.FeaturedImageUrl == null ? string.Empty : newPostData.FeaturedImageUrl;
        
        

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

    public async Task ChangeFavoritePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {postId} does not exist");
        }
        post.IsFavourite = !post.IsFavourite;
        post.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        await _context.SaveChangesAsync();
    }
}
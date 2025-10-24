using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    
    public async Task AddNewPostAsync(AddOrEditPostDto addPostDto)
    {
        DateTime currentDateTime = DateTime.UtcNow;
        var post = new Post
        {
            Title = addPostDto.Title,
            Slug = addPostDto.Slug,
            Content = addPostDto.Content,
            IsPublished = addPostDto.IsPublished,
            CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
        };
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> EditPostAsync(AddOrEditPostDto newPostData, int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        if (post ==  null)
            throw new Exception("File you are trying to edit is not existing.");
        post.Title = newPostData.Title;
        post.Slug = newPostData.Slug;
        post.Content = newPostData.Content;
        post.IsPublished = newPostData.IsPublished;
        post.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> GetWholePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        if (post ==  null)
            throw new Exception("File you are trying to edit is not existing.");
        return post;
    }

    public async Task<List<ShortPostModelDto>> GetListOfPosts(int pageNumber, int pageSize)
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
    
    public async Task DeletePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        if (post == null)
            throw new Exception("Post you are trying to delete does not exist.");
    
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}
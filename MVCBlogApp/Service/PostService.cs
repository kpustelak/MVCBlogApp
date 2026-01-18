using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Service;

public class PostService : IPostService
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;
    private IPostService _postServiceImplementation;

    public PostService(BlogDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Post> GetWholePostAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        return post;
    }

    public async Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAsync(
        int pageNumber, 
        int pageSize, 
        bool byLatests = false, 
        bool byViews = false)
    {
        if (byLatests) {
            return await _context.Posts
                .Include(x => x.FeaturedImage)
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
                    IsPublished = x.IsPublished,
                    Excerpt = x.Excerpt,
                    FeaturedImage = x.FeaturedImage,
                    Category = x.PostCategory
                })
                .ToListAsync();
        }
        return await _context.Posts
            .Include(x => x.FeaturedImage)
            .Where(x => x.IsPublished)
            .OrderBy(x => x.ViewCount)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ShortPostModelDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt,
                IsPublished = x.IsPublished,
                Excerpt = x.Excerpt,
                FeaturedImage = x.FeaturedImage,
                Category = x.PostCategory
            })
            .ToListAsync();
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
                FeaturedImage = x.FeaturedImage,
                Category = x.PostCategory
            })
            .ToListAsync();
        return posts;
    }

    public async Task<GetPostModelDto> GetPostByIdAsync(int postId)
    {
        return _mapper.Map<GetPostModelDto>(await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId));
    }

    public async Task<List<ShortPostModelDto>> GetListOfPostsByQueryAsync(int pageNumber, int pageSize, string query)
    {
        var posts = await _context.Posts
            .Where(x => x.Title.ToLower().Contains(query.ToLower()))
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ShortPostModelDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt,
                IsPublished = x.IsPublished,
                Excerpt = x.Excerpt,
                FeaturedImage = x.FeaturedImage,
                Category = x.PostCategory
            })
            .ToListAsync();
        return posts;
    }

    public async Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAndCategoryAsync(int pageNumber, int pageSize, int categoryId)
    {
        var posts = await _context.Posts
            .Where(x => x.IsPublished && x.PostCategoryId == categoryId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ShortPostModelDto
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CreatedAt = x.CreatedAt,
                IsPublished = x.IsPublished,
                Excerpt = x.Excerpt,
                FeaturedImage = x.FeaturedImage,
                Category = x.PostCategory
            })
            .ToListAsync();

        return posts;
    }
}
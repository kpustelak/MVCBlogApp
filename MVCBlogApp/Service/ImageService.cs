using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
using MVCBlogApp.Models.DTO.PostImage;

namespace MVCBlogApp.Service;

public class ImageService : IImageService
{
    private readonly BlogDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ImageService> _logger;
    public ImageService(BlogDbContext context, IWebHostEnvironment environment, ILogger<ImageService> logger)
    {
        _context = context;
        _environment = environment;
        _logger = logger;
    }
    public async Task<PostImage> UploadImageAsync(AddPostImageRequest request)
    {
        if (!IsValidImageExtension(request.File.FileName))
            throw new InvalidOperationException("There is something wrong with file format");
        
        if (request.File.Length > 5 * 1024 * 1024)
            throw new InvalidOperationException("File is too big (max 5MB)");
        
        var fileName = GenerateUniqueFileName(request.File.FileName);
        
        var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadPath);
        
        var filePath = Path.Combine(uploadPath, fileName);
        
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }
        
        var image = new PostImage
        {
            FileName = request.File.FileName,
            FilePath = $"/uploads/{fileName}",
            AltText = request.AltText,
            FileSize = request.File.Length,
            FileExtension = Path.GetExtension(request.File.FileName),
            UploadDate = DateTime.UtcNow 
        };
        
        await _context.PostImages.AddAsync(image);
        await _context.SaveChangesAsync();
        
        return image;
    }
    public async Task DeleteImageAsync(int id)
    {
        var image = await _context.PostImages.FindAsync(id);
        if (image == null) throw new KeyNotFoundException($"Image ID {id} not found");

        var root = _environment.WebRootPath;
        var physicalPath = Path.GetFullPath(Path.Combine(root, image.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));

        if (!physicalPath.StartsWith(root))
            throw new UnauthorizedAccessException("Attempted to delete file outside of web root.");

        _context.PostImages.Remove(image);
        await _context.SaveChangesAsync();

        try
        {
            if (File.Exists(physicalPath)) File.Delete(physicalPath);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "Physical file deletion failed for ID {Id} at {Path}", id, physicalPath);
        }
    }
    public async Task<int> GetTotalImageCountAsync()
    {
        return await _context.PostImages.CountAsync();
    }
    public async Task<List<PostImage>> GetImageDataListAsync(int page, int pageSize)
    {
        return await _context.PostImages
            .OrderByDescending(x => x.UploadDate) 
            .Skip((page - 1) * pageSize)          
            .Take(pageSize)
            .ToListAsync();                         
    }
    public async Task EditImageAltTextAsync(int id, string fileText)
    {
        var image = _context.PostImages.FirstOrDefault(x => x.Id == id);
        if (image == null)
            throw new InvalidOperationException($"Image with ID {id} not found");
        image.AltText = fileText;
        await _context.SaveChangesAsync();
    }
    private bool IsValidImageExtension(string fileName)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(fileName).ToLower();
        return allowedExtensions.Contains(extension);
    }
    private string GenerateUniqueFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid()}{extension}";
    }
}
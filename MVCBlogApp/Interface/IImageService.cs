using MVCBlogApp.Models;
using MVCBlogApp.Models.DTO.PostImage;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IImageService
{
    Task<PostImage> UploadImageAsync(AddPostImageRequest postImage);
    Task DeleteImageAsync(int id);
    Task<PostImage> GetImageDataAsync(int id);
    Task<List<PostImage>> GetImageDataListAsync(int page, int pageSize);
    Task<int> GetTotalImageCountAsync();
}
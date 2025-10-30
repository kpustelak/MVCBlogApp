using AutoMapper;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Mapping;

public class PostProfile : AutoMapper.Profile
{
    public PostProfile()
    {
        CreateMap<Post, AddOrEditPostDto>().ReverseMap();
    }
}
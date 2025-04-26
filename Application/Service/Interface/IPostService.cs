using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Application.Dto.PageDtos;
using Application.Dto.PostDtos;

namespace Application.Service.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<IndexPostDto>> GetAllPostAsync();
        Task<DetailPostDto> GetByIdAsync(int id);
        Task<bool> AddPostAsync(CreatePostDto createPostDto);
        Task<bool> DeletePostAsync(int postId);
        Task<UpdatePostDto> GetPostForUpdateAsync(int postId);
        Task<bool> UpdatePostAsync(UpdatePostDto updatePostDto);
        Task<bool> PublishPostAsync(int id);
        Task<IEnumerable<IndexPostDto>> GetAllPostForUserAsync();
        Task<List<IndexPostDto>> GetPostByTagForUserAsync(string tagName);
        Task<List<IndexPostDto>> GetPostByCategoryForUserAsync(int id);
        Task<List<IndexPostDto>> GetMyPostAsync();
        Task<List<IndexPostDto>> GetTopViewAsync(int top);
        Task<DetailPostDto> FindPost(int year, int month, string title);
        Task<PageResultDto<IndexPostDto>> GetPostPageAsync(int page, int pageSize);
        Task<PageResultDto<IndexPostDto>> GetPostByFilterAsync(string sapxep, string ispublish, int index, int pageSize);
    }
}

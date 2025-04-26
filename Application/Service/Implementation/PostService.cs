using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Application.Dto.PageDtos;
using Application.Dto.PostDtos;
using Application.Helper;
using Application.Service.Interface;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Application.Service.Implementation
{
    public class PostService : IPostService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ITagService _tagService;
        protected readonly IAuthService _authService;
        protected readonly IBaseEntityService<Post> _baseEntityService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ITagService tagService, IAuthService authService, IBaseEntityService<Post> baseEntityService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tagService = tagService;
            _authService = authService;
            _baseEntityService = baseEntityService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddPostAsync(CreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);
            _baseEntityService.Add(post);
            await _unitOfWork.Posts.AddAsync(post);
            var result = await _unitOfWork.SaveChange();
            // Xử lý nhiều loại ký tự phân tách
            char[] delimiters = { ' ', ',', ';', '\t' };
            await _tagService.AddTag(post.Id, createPostDto.TagNames.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(tag => tag.Trim())
                                             .Where(tag => !string.IsNullOrEmpty(tag))
                                             .ToList());
            return result >= 1;

        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var checkRight = _authService.IsAdmin();
            var post = await _unitOfWork.Posts.GetByIdAsync(postId);
            var checkOwner = _authService.IsOwner(post.CreatedBy ?? 0);
            if (checkRight || checkOwner)
            {
                throw new Exception("You don't have right for this action");
            }
            await _unitOfWork.Posts.Delete(postId);
            return await _unitOfWork.SaveChange() >= 1;
        }

        public async Task<DetailPostDto> FindPost(int year, int month, string title)
        {
            title = title.Replace("-", " ");

            var posts = await _unitOfWork.Posts.GetAll()
                .Where(p => p.CreatedDate.Year == year)
                .Where(p => p.CreatedDate.Month == month)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                //.Include(p => p.PostRates)
                .ToListAsync();

            var post = posts.FirstOrDefault(p =>
                p.Title.RemoveDiacritics().ToLower() == title.ToLower());

            // Map to DTO
            var postDto = _mapper.Map<DetailPostDto>(post);
            return postDto;
        }

        public async Task<IEnumerable<IndexPostDto>> GetAllPostAsync()
        {
            var posts = await _unitOfWork.Posts.GetAll()
                .Include(p => p.CreatedByUser)
                .Include(p => p.PostRates).ToListAsync();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(posts);
            return indexPostDtos;
        }

        public async Task<IEnumerable<IndexPostDto>> GetAllPostForUserAsync()
        {
            var posts = await _unitOfWork.Posts.GetAll()
                .Where(p => p.IsPublished)
                .Include(p => p.CreatedByUser)
                .Include(p => p.PostRates).ToListAsync();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(posts);
            return indexPostDtos;
        }

        public async Task<DetailPostDto> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            await _unitOfWork.Comments.GetCommentsByPostId(id);
            post.Views++;
            await _unitOfWork.SaveChange();
            var detailPostDto = _mapper.Map<DetailPostDto>(post);
            return detailPostDto;
        }

        public Task<List<IndexPostDto>> GetMyPostAsync()
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity
                          ?? throw new Exception("jwt is invalid");
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? throw new Exception("User ID not found in token");
            var post = _unitOfWork.Posts.GetAll()
                .Where(p => p.CreatedBy == Convert.ToInt32(userId))
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.PostRates)
                .ToList();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(post);
            return Task.FromResult(indexPostDtos);
        }

        public async Task<List<IndexPostDto>> GetPostByCategoryForUserAsync(int id)
        {
            var post = await _unitOfWork.Posts.GetAll()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == id)
                .Where(p => p.IsPublished)
                .Include(p => p.CreatedByUser)
                .ToListAsync();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(post);
            return indexPostDtos;
        }

        public async Task<PageResultDto<IndexPostDto>> GetPostByFilterAsync(string sapxep, string ispublish, int index, int pageSize)
        {
            //sapxep = latest oldest most-viewed least-viewed
            // ispublish = published unpublished all

            // Khởi tạo query
            IQueryable<Post> query = _unitOfWork.Posts.GetAll();

            switch (ispublish?.ToLower())
            {
                case "published":
                    query = query.Where(p => p.IsPublished == true);
                    break;
                case "unpublished":
                    query = query.Where(p => p.IsPublished == false);
                    break;
                case "all":
                default:
                    break;
            }

            // Sắp xếp
            switch (sapxep?.ToLower())
            {
                case "latest":
                    query = query.OrderByDescending(p => p.CreatedDate);
                    break;
                case "oldest":
                    query = query.OrderBy(p => p.CreatedDate);
                    break;
                case "most-viewed":
                    query = query.OrderByDescending(p => p.Views);
                    break;
                case "least-viewed":
                    query = query.OrderBy(p => p.Views);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedDate);
                    break;
            }

            // Đếm tổng số bản ghi thỏa mãn điều kiện
            int totalCount = await query.CountAsync();

            // Phân trang và lấy dữ liệu
            var posts = await query
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Chuyển đổi từ entity sang DTO
            var postDtos = _mapper.Map<List<IndexPostDto>>(posts);

            // Trả về kết quả với thông tin phân trang
            return new PageResultDto<IndexPostDto>
            {
                Items = postDtos,
                Page = index,
                PageSize = pageSize,
                TotalItems = totalCount
            };
        }

        public async Task<List<IndexPostDto>> GetPostByTagForUserAsync(string tagName)
        {
            var post = await _unitOfWork.Posts.GetAll()
                .Include(p => p.Tags)
                .Where(p => p.Tags.Any(t => t.TagName == tagName))
                .Where(p => p.IsPublished)
                .ToListAsync();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(post);
            return indexPostDtos;
        }

        public async Task<UpdatePostDto> GetPostForUpdateAsync(int postId)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId);
            var updatePostDto = _mapper.Map<UpdatePostDto>(post);
            return updatePostDto;
        }

        public async Task<PageResultDto<IndexPostDto>> GetPostPageAsync(int page, int pageSize)
        {
            var posts = _unitOfWork.Posts.GetAll()
                        .Include(p => p.CreatedByUser)
                        .Include(p => p.PostRates)
                        .Include(p => p.Category)
                        .Include(p => p.Tags)
                        .OrderByDescending(p => p.CreatedDate);
            
            int totalItem = posts.Count();

            var data = await posts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PageResultDto<IndexPostDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItem,
                Items = _mapper.Map<List<IndexPostDto>>(data)
            };

        }

        public async Task<List<IndexPostDto>> GetTopViewAsync(int top)
        {
            var post = _unitOfWork.Posts.GetAll()
                .OrderByDescending(p => p.Views)
                .Take(top)
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .Include(p => p.PostRates)
                .ToList();
            var indexPostDtos = _mapper.Map<List<IndexPostDto>>(post);
            return indexPostDtos;
        }

        public async Task<bool> PublishPostAsync(int id)
        {
            //get post and update publish status
            var post = _unitOfWork.Posts.GetByIdAsync(id).Result;
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            post.IsPublished = !post.IsPublished;

            _baseEntityService.Update(post);
            await _unitOfWork.Posts.Update(post);

            // Lưu thay đổi
            return await _unitOfWork.SaveChange() >= 1;
        }



        public async Task<bool> UpdatePostAsync(UpdatePostDto updatePostDto)
        {
            // check createdby
            bool checkOwner = _authService.IsOwner(updatePostDto.CreatedBy);

            if (checkOwner == false)
            {
                throw new Exception("You are not owner of this post");
            }
            // Tìm post hiện tại trong database
            var existingPost = await _unitOfWork.Posts
                .GetByIdAsync(updatePostDto.Id);

            if (existingPost == null)
                return false;

            // Ánh xạ các thuộc tính từ DTO sang entity
            _mapper.Map(updatePostDto, existingPost);

            // Xóa các tag cũ
            existingPost.Tags.Clear();

            // Thêm các tag mới
            if (!string.IsNullOrWhiteSpace(updatePostDto.TagNames))
            {
                var tagNames = updatePostDto.TagNames
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Distinct()
                    .ToList();

                foreach (var tagName in tagNames)
                {
                    // Kiểm tra và thêm tag mới
                    var tag = new Tag
                    {
                        TagName = tagName.Trim(),
                        PostId = existingPost.Id
                    };
                    existingPost.Tags.Add(tag);
                }
            }

            // Cập nhật post
            _baseEntityService.Update(existingPost);
            await _unitOfWork.Posts.Update(existingPost);

            // Lưu thay đổi
            return await _unitOfWork.SaveChange() >= 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application.Dto.CommentDtos;
using Application.Service.Interface;
using AutoMapper;
using Domain.Entity;
using Infrastructure.UnitOfWork;

namespace Application.Service.Implementation
{
    public class CommentService : ICommentService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IBaseEntityService<Comment> _baseEntityService;
        protected readonly IAuthService _authService;


        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IBaseEntityService<Comment> baseEntityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _baseEntityService = baseEntityService;
        }

        public async Task<bool> CreateComment(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            _baseEntityService.Add(comment);
            await _unitOfWork.Comments.AddAsync(comment);
            return await _unitOfWork.SaveChange() >= 1;

        }

        public async Task<bool> DeleteComment(int commentId)
        {
            if (_authService.IsOwner(commentId))
            {
                await _unitOfWork.Comments.Delete(commentId);
                return await _unitOfWork.SaveChange() >= 1;
            }
            throw new Exception("You don't have right for this action");
        }
    }
}

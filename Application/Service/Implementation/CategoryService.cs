using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;
using Application.Service.Interface;
using AutoMapper;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using Microsoft.Identity.Client;

namespace Application.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IBaseEntityService<Category> _baseEntityService;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IBaseEntityService<Category> baseEntityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _baseEntityService = baseEntityService;
        }

        public async Task<bool> AddCategoryAsync(CreateCategoryDto categoryName)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryName);
                _baseEntityService.Add(category);
                await _unitOfWork.Categories.AddAsync(category);
                return await _unitOfWork.SaveChange() >= 1;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            await _unitOfWork.Categories.Delete(categoryId);
            return await _unitOfWork.SaveChange() >= 1;
        }

        public async Task<List<IndexCategoryDto>> GetAllCategoryAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<List<IndexCategoryDto>>(categories);
        }

        public async Task<IndexCategoryDto> GetByIdCategoryAsync(int categoryId)
        {
             var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            return _mapper.Map<IndexCategoryDto>(category);
        }

        public async Task<UpdateCategoryDto> GetCategoryForUpdateAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            return _mapper.Map<UpdateCategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
                var category = await _unitOfWork.Categories.GetByIdAsync(updateCategoryDto.Id);
                _mapper.Map(updateCategoryDto, category);
                _baseEntityService.Update(category);
                await _unitOfWork.Categories.Update(category);
                return await _unitOfWork.SaveChange() >= 1;
        }
    }
}

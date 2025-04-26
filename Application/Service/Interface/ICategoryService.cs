using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.CategoryDtos;

namespace Application.Service.Interface
{
    public interface ICategoryService
    {
        Task<IndexCategoryDto> GetByIdCategoryAsync(int categoryId);
        Task<List<IndexCategoryDto>> GetAllCategoryAsync();
        Task<bool> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<UpdateCategoryDto> GetCategoryForUpdateAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    }
}

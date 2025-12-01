using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync(bool trackChanges);
        Task<CategoryDto> GetCategoryAsync(int categoryId, bool trackChanges);
        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto categoryForCreationDto);
        Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto categoryForUpdateDto, bool trackChanges);
        Task DeleteCategoryAsync(int categoryId, bool trackChanges);
    }
}

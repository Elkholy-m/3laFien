using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges);
        Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges);
        void CreateCategoryAsync(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}

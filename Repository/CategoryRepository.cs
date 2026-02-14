using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<RepositoryContext, Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }
        public void CreateCategoryAsync(Category category) => Create(category);

        public void DeleteCategory(Category category) => Delete(category);

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges) =>
            await FindByCondition(c => c.IsDeleted == false, trackChanges).ToListAsync();


        public async Task<Category?> GetCategoryAsync(int categoryId, bool trackChanges) =>
            await FindByCondition(c => c.CategoryId == categoryId && c.IsDeleted == false, trackChanges).SingleOrDefaultAsync();

        public void UpdateCategory(Category category) => Update(category);
    }
}

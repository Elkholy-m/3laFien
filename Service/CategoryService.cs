using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DTO;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto categoryForCreationDto)
        {
            var category = _mapper.Map<Category>(categoryForCreationDto);
            _repositoryManager.Category.CreateCategoryAsync(category);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsync(int categoryId, bool trackChanges)
        {
            var category = await CheckCategoryExistance(categoryId, trackChanges);
            _repositoryManager.Category.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(bool trackChanges)
        {
            var categories = await _repositoryManager.Category.GetCategoriesAsync(trackChanges);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId, bool trackChanges)
        {
            var category = await CheckCategoryExistance(categoryId, trackChanges);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto categoryForUpdateDto, bool trackChanges)
        {
            var category = await CheckCategoryExistance(categoryId, trackChanges);
            _mapper.Map(categoryForUpdateDto, category);
            _repositoryManager.Category.UpdateCategory(category);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Category> CheckCategoryExistance(int categoryId, bool trackChanges)
        {
            var category = await _repositoryManager.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
            return category;
        }
    }
}

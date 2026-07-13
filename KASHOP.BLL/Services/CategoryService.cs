using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task< CategoryResponse> CreateCategory(CategoryRequest categoryRequest)
        {
            var category = categoryRequest.Adapt<Category>();
            await  _categoryRepository.createAsync(category);
            return category.Adapt<CategoryResponse>();


        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var catiegories = await _categoryRepository.getAllAsync( new string[]
            {
                nameof(Category.Translations)
            });
            var response = catiegories.Adapt<List<CategoryResponse>>();
            return response;
        }
    }
}

using KASHOP.BLL.Services;
using KASHOP.DAL.Data;
using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository;
using KASHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;
        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResources> localizer, ICategoryService categoryService)
        {
            _context = context;
            _localizer = localizer;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();

            return Ok(new {
                _localizer["sucses"].Value,
                categories
            });
        }
        [HttpPost("")]
        public async Task<IActionResult> Create( CategoryRequest request ) {
        
            var response =await _categoryService.CreateCategory(request);
            return Ok();

        }
        [HttpGet("{id}")]
        public async   Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategory(c => c.Id == id);
            return Ok(category);
        }
        [HttpDelete("{id}")]        
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

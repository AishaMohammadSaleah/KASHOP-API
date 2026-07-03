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

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResources> localizer, ICategoryRepository categoryRepository)
        {
            _context = context;
            _localizer = localizer;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
              var categories = _categoryRepository.getAll();
            var response = categories.Adapt<List<CategoryResponse>>();

            return Ok(new {
                _localizer["sucses"].Value,response });
        }
        [HttpPost("")]
        public IActionResult Create( CategoryRequest request ) {
        
            var category =request.Adapt<Category>();
            _categoryRepository.create(category);
            return Ok();

        }
       
    }
}

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Prompt.Domain.Entities;
using Prompt.Persistence.EntityFramework.Contexts;
using Prompt.WebApi.V1.Categories.Queries.GetAll;
using Prompt.WebApi.V1.Categories.Queries.GetById;
using System.Linq;

namespace Prompt.WebApi.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _dbContext;

        public CategoriesController(IMemoryCache memoryCache,
       ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellation)
        {
            var categories = await _dbContext
                .Categories
                .AsNoTracking()
                .Select(category => new GetAllCategoriesDto(category.Id, category.Name))
                .ToListAsync(cancellation);


            return Ok(categories);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id,CancellationToken cancellation)
        {
            var category=await _dbContext
                .Categories
                .AsNoTracking()
                .Select(category => new GetByIdCategoryDto(category.Id,category.Name,category.Description))
                .FirstOrDefaultAsync(category=> category.Id == id,cancellation);

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto, CancellationToken cancellation)
        {
            var category = Category.Create(dto.Name, dto.Description);
             _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellation);
            return Ok(category);

        }
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync(long id,UpdateCategoryDto dto, CancellationToken cancellation)
        {
            if (dto.Id != id) return BadRequest();
           var category =await _dbContext
                .Categories
                .FirstOrDefaultAsync(category => category.Id == id,cancellation);

            if(category is null) return NotFound();

            category.Name= dto.Name;
            category.Description= dto.Description;
            await _dbContext.SaveChangesAsync(cancellation);

            return Ok(category);
        }
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellation)
        {
            // Kategorinin varlığını kontrol et
            var category = await _dbContext
                .Categories
                .FirstOrDefaultAsync(category => category.Id == id, cancellation);

            if (category is null)
                return NotFound(new { Message = "Category not found" });
  
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync(cancellation);
            return NoContent();
        }
    }
}




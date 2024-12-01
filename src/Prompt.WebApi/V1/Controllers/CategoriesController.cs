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
        private const string _allCategoriesCacheKey = "all-categories";
        private const string _categoryKeyCachePrefix = "category-";
        private  readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _dbContext;

        public CategoriesController(IMemoryCache memoryCache,
       ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;

            var slidingExpiration=TimeSpan.FromMinutes(10);
            var absoluteExpiration = TimeSpan.FromMinutes(24);

            _cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(slidingExpiration)
                .SetAbsoluteExpiration(absoluteExpiration);
        
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellation)
        {
            if(_memoryCache.TryGetValue(_allCategoriesCacheKey,out List<GetAllCategoriesDto> cachedCategories)) return Ok(cachedCategories);

            var categories = await _dbContext
                .Categories
                .AsNoTracking()
                .Select(category => new GetAllCategoriesDto(category.Id, category.Name))
                .ToListAsync(cancellation);

            _memoryCache.Set(_allCategoriesCacheKey,categories,_cacheOptions);
            return Ok(categories);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id,CancellationToken cancellation)
        {
            var cacheKey = $"{_categoryKeyCachePrefix}{id}";

            if (_memoryCache.TryGetValue(cacheKey, out GetByIdCategoryDto cachedCategory)) return Ok(cachedCategory);

            var category=await _dbContext
                .Categories
                .AsNoTracking()
                .Select(category => new GetByIdCategoryDto(category.Id,category.Name,category.Description))
                .FirstOrDefaultAsync(category=> category.Id == id,cancellation);

            _memoryCache.Set(cacheKey,category,_cacheOptions);  
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryDto dto, CancellationToken cancellation)
        {
            var category = Category.Create(dto.Name, dto.Description);
             _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellation);
            InvalidateCache();
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
            InvalidateCache(id);

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
            InvalidateCache(id);
            return NoContent();
        }

        private void InvalidateCache(long? categoryId = null)
        {
            _memoryCache.Remove(_allCategoriesCacheKey);
            if (categoryId.HasValue)
            {
                _memoryCache.Remove($"{_categoryKeyCachePrefix}{categoryId}");
            }
        }
    }
}




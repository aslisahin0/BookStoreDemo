using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Service;
using BookStore.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(new FunctionResultWithData<IEnumerable<CategoryDto>>
            {
                IsSuccess = true,
                Message = "Tüm kategoriler başarıyla listelendi.",
                Data = result
            });
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kategori bulunamadı."
                });
            }
            return Ok(new FunctionResultWithData<CategoryDto>
            {
                IsSuccess = true,
                Message = "Kategori başarıyla getirildi.",
                Data = result
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return Ok(new FunctionResultWithData<CategoryDto>
            {
                IsSuccess = true,
                Message = "Kategori başarıyla oluşturuldu.",
                Data = result
            });
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryService.UpdateAsync(id, dto);
            if (!result)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kategori bulunamadı."
                });
            }
            return Ok(new FunctionResult
            {
                IsSuccess = true,
                Message = "Kategori başarıyla güncellendi."
            });
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kategori bulunamadı."
                });
            }
            return Ok(new FunctionResult
            {
                IsSuccess = true,
                Message = "Kategori başarıyla silindi."
            });
        }
    }
}

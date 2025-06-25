using BookStore.Application.DTOs;
using BookStore.Application.Interfaces.Service;
using BookStore.Core.Entities;
using BookStore.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();
            return Ok(new FunctionResultWithData<IEnumerable<BookDto>>
            {
                IsSuccess = true,
                Message = "Tüm kitaplar başarıyla listelendi.",
                Data = result
            });
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bookService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kitap bulunamadı.",
                });
            }
            return Ok(new FunctionResultWithData<BookDto>
            {
                IsSuccess = true,
                Message = "Kitap başarıyla getirildi.",
                Data = result
            });
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await _bookService.GetByCategoryAsync(categoryId);
            if (result == null)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kategori bulunamadı.",
                });
            }
            return Ok(new FunctionResultWithData<IEnumerable<BookDto>>
            {
                IsSuccess = true,
                Message = "Kategoriye göre kitaplar başarıyla getirildi.",
                Data = result
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var result = await _bookService.CreateAsync(dto);
            return Ok(new FunctionResultWithData<CreateBookDto>
            {
                IsSuccess = true,
                Message = "Kitap başarıyla oluşturuldu.",
                Data = result
            });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            
            var result = await _bookService.UpdateAsync(id, dto);
            if (!result)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kitap bulunamadı."
                });
            }
            return Ok(new FunctionResult
            {
                IsSuccess = true,
                Message = "Kitap başarıyla güncellendi."
            });
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            if (!result)
            {
                return NotFound(new FunctionResult
                {
                    IsSuccess = false,
                    Message = "Kitap bulunamadı."
                });
            }

            return Ok(new FunctionResult
            {
                IsSuccess = true,
                Message = "Kitap başarıyla silindi."
            });
        }
    }
}

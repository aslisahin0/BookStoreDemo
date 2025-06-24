using BookStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CreateCategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Service;
using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class CategoryService : Service, ICategoryService
    {
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<CreateCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _unitOfWork.CategoryRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateCategoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.CategoryRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _unitOfWork.CategoryRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

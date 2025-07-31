using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Service;
using BookStore.Core.Entities;
using BookStore.Core.ValueObjects.BookStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class BookService : Service, IBookService
    {
        private readonly IMapper _mapper;


        public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new ArgumentException("Geçersiz kategori.");

            var author = new Author(dto.Author.FirstName, dto.Author.LastName);
            var price = new Price(dto.Price.Amount, dto.Price.Currency);

            var book = Book.Create(dto.Title, author, price, category);

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.BookRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetByCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
            if (category == null) return null;

            var books = await _unitOfWork.BookRepository.GetByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var entity = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (entity == null) return false;

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new ArgumentException("Geçersiz kategori.");

            // Güncellemeleri entity üzerinden yap (DDD prensibi)
            entity.UpdateTitle(dto.Title);
            entity.UpdatePrice(new Price(dto.Price.Amount, dto.Price.Currency));
            entity.ChangeCategory(category);

            // Author değiştirilebilecekse bu şekilde güncelle
            entity.ChangeAuthor(new Author(dto.Author.FirstName, dto.Author.LastName));

            _unitOfWork.BookRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

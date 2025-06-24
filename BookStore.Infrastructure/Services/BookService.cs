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
    public class BookService : Service, IBookService
    {
        private readonly IMapper _mapper;


        public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<CreateBookDto> CreateAsync(CreateBookDto dto)
        {
            var entity = _mapper.Map<Book>(dto);
            await _unitOfWork.BookRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CreateBookDto>(entity);
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

        public  async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var entity = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _unitOfWork.BookRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

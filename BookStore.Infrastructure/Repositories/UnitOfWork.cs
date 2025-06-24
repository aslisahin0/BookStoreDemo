using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Repository;
using BookStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBookRepository BookRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(AppDbContext context,IBookRepository bookRepository,ICategoryRepository categoryRepository)
        {
            _context = context;
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

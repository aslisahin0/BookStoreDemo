using BookStore.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> SaveChangesAsync();
        void Dispose();
    }
}

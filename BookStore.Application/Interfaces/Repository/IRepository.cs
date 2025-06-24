using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces.Repository
{
    public interface IRepository <T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> Get(Expression<Func<T, bool>> predicate); //Şarta göre kayıt getirir
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

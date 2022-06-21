using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T item);

        void Update(T item);

        void SetValues(T OldItem, T NewItem);

        void Delete(T item);

        Task SaveChangesAsync();
    }
}

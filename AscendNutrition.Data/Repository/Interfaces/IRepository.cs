using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);

       Task<TType> GetByIdAsync(TId id);

       TType FirstOrDefault(Func<TType, bool> predicate);

       Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);
       
        IEnumerable<TType> GetAll();

        Task<IEnumerable<TType>> GetAllAsync();

        IQueryable<TType> GetAllAttached();

        void Add(TType entity);

        Task AddAsync(TType entity);

        bool Delete(object id);

        Task<bool> DeleteAsync(object id);

        bool Update(TType entity);

        Task<bool> UpdateAsync(TType entity);
    }
}

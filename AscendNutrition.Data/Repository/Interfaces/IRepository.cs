using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);

       Task<TType> GetByIdAsync(TId id);

        IEnumerable<TType> GetAll();

        Task<IEnumerable<TType>> GetAllAsync();

        IEnumerable<TType> GetAllAttached();

        void Add(TType entity);

        Task AddAsync(TType entity);

        bool Delete(TId id);

        Task<bool> DeleteAsync(TId id);

        void Update(TType entity);

        Task UpdateAsync(TType entity);
    }
}

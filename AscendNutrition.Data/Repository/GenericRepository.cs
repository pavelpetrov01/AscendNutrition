using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AscendNutrition.Data.Repository
{
    public class GenericRepository<TType, TId> : IRepository<TType, TId> where TType : class
    {
        private readonly AscendNutritionDbContext _context;
        private readonly DbSet<TType> _set;

        public GenericRepository(AscendNutritionDbContext context)
        {
            _context = context;
            _set = _context.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = _set.Find(id);
            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
           TType entity = await _set.FindAsync(id);
            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return _set.ToList();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }
        public IEnumerable<TType> GetAllAttached()
        {
            return _set.AsQueryable();
        }

        public void Add(TType entity)
        {
            _set.Add(entity);
            _context.SaveChanges();
        }

        public async Task AddAsync(TType entity)
        {
             await _set.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public bool Delete(TId id)
        {
            TType entity = GetById(id);
            if (entity == null)
            {
                return false;
            }

            _set.Remove(entity);
            _context.SaveChanges();
            return true;
        }

       

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _set.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public void Update(TType entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TType entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }

       
    }
}

using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// 2.6 - Реализовать репозитории под все сущности кроме референсных (ManyToMany)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await Apply(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await Apply(spec).ToListAsync();
        }

        public T Add(T entity)
        {
            EntityEntry<T> addEntiry = _context.Set<T>().Add(entity);
            return addEntiry.Entity;
        }

        public T Update(T entity)
        {
            EntityEntry<T> addEntiry = _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return addEntiry.Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        private IQueryable<T> Apply(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy != null)
            {
                var orderQuery = query.OrderBy(spec.OrderBy.First());
                if (spec.OrderBy.Count > 1)
                {
                    foreach (var thenBy in spec.OrderBy.Skip(1))
                        orderQuery = orderQuery.ThenBy(thenBy);
                }
                query = orderQuery;
            }
            if(spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
}

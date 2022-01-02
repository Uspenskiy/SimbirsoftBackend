using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private Hashtable _repositories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(T).Name;
            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mock
{
    /// <summary>
    /// Класс отвечающий за хранение и работу со списком людей (Human)
    /// </summary>
    public class MockHumanRepository : IGenericRepository<Human>
    {
        private List<Human> _humans;
        public MockHumanRepository()
        {
            _humans = MockContext.Humans;
        }

        /// <summary>
        /// Получение конкретного человека по id
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<Human> GetByIdAsync(int id)
        {
            return _humans.FirstOrDefault(s => s.Id == id);
        }

        public Task<Human> GetEntityWithSpec(ISpecification<Human> spec)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<Human> GetEntityWithSpec(ISpecification<Book> spec)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Список всех людей
        ///// </summary>
        ///// <param name="searchParams"></param>
        ///// <returns></returns>
        //public async Task<IReadOnlyList<Human>> ListAsync(string searchParams)
        //{
        //    return _humans.Where(s => s.Name.Contains(searchParams) ||
        //                        s.Surname.Contains(searchParams) ||
        //                        s.Patronymic.Contains(searchParams))
        //                   .ToList();
        //}

        public Task<IReadOnlyList<Human>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Human>> ListAsync(ISpecification<Human> spec)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Добавление человека
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<Human> AddAsync(Human entity)
        {
            entity.Id = _humans.Count != 0
                ? _humans.Last().Id + 1
                : 0;
            _humans.Add(entity);
            return entity;
        }

        /// <summary>
        /// Удаление человека
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Human entity)
        {
            _humans.Remove(entity);
        }
    }
}

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

        public async Task<Human> GetEntityWithSpec(ISpecification<Human> spec)
        {
            return SpecificationEvaluator<Human>.Apply(_humans, spec)
                .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<Human>> ListAllAsync()
        {
            return _humans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Human>> ListAsync(ISpecification<Human> spec)
        {
            return SpecificationEvaluator<Human>.Apply(_humans, spec)
                .ToList();
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

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
    /// 1.2.1 - Класс отвечающий за хранение и работу со списком людей (Human)
    /// </summary>
    public class MockHumanRepository : IHumanRepository
    {
        private List<Human> _humans;
        public MockHumanRepository()
        {
            _humans = MockContext.Humens;
        }

        /// <summary>
        /// 1.2.1 - Список всех людей
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Human>> ListAsync(string searchParams)
        {
            return _humans.Where(s => s.Name.Contains(searchParams) ||
                                s.Surname.Contains(searchParams) ||
                                s.Patronymic.Contains(searchParams))
                           .ToList();
        }

        /// <summary>
        /// 1.2.1 - Получение конкретного человека по id
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<Human> GetByIdAsync(int id)
        {
            return _humans.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// 1.2.1 - Добавление человека
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task AddAsync(Human entity)
        {
            entity.Id = _humans.Count();
            _humans.Add(entity);
        }

        /// <summary>
        /// 1.2.1 - Удаление человека
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Human entity)
        {
            _humans.Remove(entity);
        }
    }
}

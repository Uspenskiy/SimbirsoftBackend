using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Интерфейс описывающий методы для работы со списком людей
    /// </summary>
    public interface IHumanRepository
    {
        /// <summary>
        /// Список всех людей
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Human>> ListAsync(string searchParams);
        
        /// <summary>
        /// Получение конкретного человека по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Human> GetByIdAsync(int id);
        
        /// <summary>
        /// Добавление человека
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(Human entity);
        
        /// <summary>
        /// Удаление человека
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Human entity);
    }
}

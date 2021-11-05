using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// 1.2.1 - Интерфейс описывающий методы для работы со списком людей
    /// </summary>
    public interface IHumanRepository
    {
        /// <summary>
        /// 1.2.1 - Список всех людей
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        Task<IReadOnlyList<Human>> ListAsync(string searchParams);
        
        /// <summary>
        /// 1.2.1 - Получение конкретного человека по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Human> GetByIdAsync(int id);
        
        /// <summary>
        /// 1.2.1 - Добавление человека
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(Human entity);
        
        /// <summary>
        /// 1.2.1 - Удаление человека
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Human entity);
    }
}

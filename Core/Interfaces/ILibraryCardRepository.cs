using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Интерфейс описывающий методы для работы со списком LibraryCard
    /// </summary>
    public interface ILibraryCardRepository
    {
        /// <summary>
        /// Список всех карточек
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LibraryCard> GetByIdAsync(int id);

        /// <summary>
        /// Получение конкретной карточки по id
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<LibraryCard>> ListAllAsync();

        /// <summary>
        /// Добавление карточки
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(LibraryCard entity);

        /// <summary>
        /// Удаление карточки
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(LibraryCard entity);
    }
}

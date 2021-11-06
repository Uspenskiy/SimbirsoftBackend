using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Интерфейс описывающий методы для работы со списком книг
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Список всех книг
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetByIdAsync(int id);

        /// <summary>
        /// Получение конкретной книги по id
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<Book>> ListAllAsync();

        /// <summary>
        /// Добавление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(Book entity);

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Book entity);
    }
}

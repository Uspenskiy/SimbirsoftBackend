using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// 1.2.1 - Интерфейс описывающий методы для работы со списком книг
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// 1.2.1 - Список всех книг
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetByIdAsync(int id);

        /// <summary>
        /// 1.2.1 - Получение конкретной книги по id
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<Book>> ListAllAsync();

        /// <summary>
        /// 1.2.1 - Добавление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(Book entity);

        /// <summary>
        /// 1.2.1 - Удаление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(Book entity);
    }
}

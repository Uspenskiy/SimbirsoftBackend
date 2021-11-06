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
    /// Класс отвечающий за хранение и работу со списком книг (Book)
    /// </summary>
    public class MockBookRepository : IBookRepository
    {
        private List<Book> _books;

        public MockBookRepository()
        {
            _books = MockContext.Books;
        }

        /// <summary>
        /// Список всех книг
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<Book>> ListAllAsync()
        {
            return _books;
        }

        /// <summary>
        /// Получение конкретной книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetByIdAsync(int id)
        {
            return _books.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Добавление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(Book entity)
        {
            entity.Id = _books.Count != 0 
                ? _books.Last().Id + 1
                : 0;
            _books.Add(entity);
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Book entity)
        {
            _books.Remove(entity);
        }
    }
}

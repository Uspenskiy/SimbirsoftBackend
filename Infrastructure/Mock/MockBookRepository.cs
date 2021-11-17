using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Infrastructure.Mock
{
    /// <summary>
    /// Класс отвечающий за хранение и работу со списком книг (Book)
    /// </summary>
    public class MockBookRepository : IGenericRepository<Book>
    {
        private List<Book> _books;

        public MockBookRepository()
        {
            _books = MockContext.Books;
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
        /// Книга отвечающая условию
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<Book> GetEntityWithSpec(ISpecification<Book> spec)
        {
            return SpecificationEvaluator<Book>.Apply(_books, spec)
                .FirstOrDefault();
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
        /// Список всех книг отвечающих условию
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Book>> ListAsync(ISpecification<Book> spec)
        {
            return SpecificationEvaluator<Book>.Apply(_books, spec)
                .ToList();
        }

        /// <summary>
        /// Добавление книги
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Book> AddAsync(Book entity)
        {
            entity.Id = _books.Count != 0
                ? _books.Last().Id + 1
                : 1;
            _books.Add(entity);
            return entity;
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

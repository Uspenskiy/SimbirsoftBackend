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
    /// Класс отвечающий за хранение и работу со списком карточек (LibraryCard)
    /// </summary>
    public class MockLibraryCardRepository : IGenericRepository<LibraryCard>
    {
        private List<LibraryCard> _cards;

        public MockLibraryCardRepository()
        {
            _cards = MockContext.LibraryCards;
        }

        /// <summary>
        /// Получение конкретной карточки по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<LibraryCard> GetByIdAsync(int id)
        {
            return _cards.FirstOrDefault(i => i.Id == id);
        }

        public async Task<LibraryCard> GetEntityWithSpec(ISpecification<LibraryCard> spec)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Список всех карточек
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<LibraryCard>> ListAllAsync()
        {
            return _cards;
        }

        public async Task<IReadOnlyList<LibraryCard>> ListAsync(ISpecification<LibraryCard> spec)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление каточки
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<LibraryCard> AddAsync(LibraryCard entity)
        {
            entity.Id = _cards.Count != 0
                ? _cards.Last().Id + 1
                : 0;
            _cards.Add(entity);
            return entity;
        }

        /// <summary>
        /// Удаление каточки
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(LibraryCard entity)
        {
            _cards.Remove(entity);
        }
    }
}

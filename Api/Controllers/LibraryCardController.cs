using Core.Entities;
using Core.Interfaces;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 1.2.1*.2 - Контроллер, который отвечает за книгу.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LibraryCardController : ControllerBase
    {
        private readonly ILogger<LibraryCardController> _logger;
        private readonly ILibraryCardRepository _cardRepository;
        private readonly IHumanRepository _humanRepository;
        private readonly IBookRepository _bookRepository;

        public LibraryCardController(ILogger<LibraryCardController> logger,
            ILibraryCardRepository cardRepository,
            IHumanRepository humanRepository,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _cardRepository = cardRepository;
            _humanRepository = humanRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Метод Get возвращающий список всех карточку
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LibraryCardDto>> GetBooks()
        {
            var cards = await _cardRepository.ListAllAsync();
            return cards.Select(i => new LibraryCardDto
            {
                Id = i.Id,
                Person = new HumanDto
                {
                    Id = i.Person.Id,
                    Name = i.Person.Name,
                    Surname = i.Person.Surname,
                    Patronymic = i.Person.Patronymic,
                    Birthday = i.Person.Birthday.ToShortDateString()
                },
                Book = new BookDto 
                {
                    Id = i.Book.Id,
                    Title = i.Book.Title,
                    Genre = i.Book.Genre,
                    Author = new HumanDto
                    {
                        Id = i.Book.Author.Id,
                        Name = i.Book.Author.Name,
                        Surname = i.Book.Author.Surname,
                        Patronymic = i.Book.Author.Patronymic,
                        Birthday = i.Book.Author.Birthday.ToShortDateString()
                    }
                },
                DateTimeOffset = i.DateTimeOffset.ToString("yyyy - MM - ddTHH:mm: ss.fffzzz")
            });
        }

        /// <summary>
        /// 1.2.1*.4 - Метод POST добавляющий новую карточку
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LibraryCardDto>> CreateCard(LibraryCardDto card)
        {
            var person = await _humanRepository.GetByIdAsync(card.Person.Id);
            if(person == null)
                return BadRequest();
            var book = await _bookRepository.GetByIdAsync(card.Book.Id);
            if (book == null)
                return BadRequest();
            await _cardRepository.AddAsync(new LibraryCard 
            {
                Person = person,
                Book = book,
                DateTimeOffset = DateTime.Now
            });
            return card;
        }

        /// <summary>
        /// Метод DELETE удаляющий карточку
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null)
                return BadRequest();
            await _cardRepository.DeleteAsync(card);
            return Ok();
        }

    }
}

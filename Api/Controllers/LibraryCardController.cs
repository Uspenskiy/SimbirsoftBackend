using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IGenericRepository<LibraryCard> _cardRepository;
        private readonly IGenericRepository<Human> _humanRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public LibraryCardController(ILogger<LibraryCardController> logger,
            IMapper mapper,
            IGenericRepository<LibraryCard> cardRepository,
            IGenericRepository<Human> humanRepository,
            IGenericRepository<Book> bookRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cardRepository = cardRepository;
            _humanRepository = humanRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Метод Get возвращающий список всех карточек
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LibraryCardDto>> GetCards()
        {
            var cards = await _cardRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<LibraryCard>, IEnumerable<LibraryCardDto>>(cards);
        }

        /// <summary>
        /// 1.2.1*.4 - Метод POST добавляющий новую карточку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LibraryCardDto>> CreateCard(LibraryCardDto dto)
        {
            var person = await _humanRepository.GetByIdAsync(dto.Person.Id);
            if(person == null) return BadRequest();
            var book = await _bookRepository.GetByIdAsync(dto.Book.Id);
            if (book == null) return BadRequest();
            var card = _mapper.Map<LibraryCardDto, LibraryCard>(dto);
            var result = await _cardRepository.AddAsync(card);
            return Ok(_mapper.Map<LibraryCard, LibraryCardDto>(result));
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
            if (card == null) return BadRequest();
            await _cardRepository.DeleteAsync(card);
            return Ok();
        }

    }
}

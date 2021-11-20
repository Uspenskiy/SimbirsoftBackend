using AutoMapper;
using Core;
using Core.Entities;
using Core.Interfaces;
using Dto;
using Infrastructure.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 1.4 - Контроллер, который отвечает за книгу.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Human> _humanRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public BookController(ILogger<BookController> logger,
            IMapper mapper,
            IGenericRepository<Human> humanRepository,
            IGenericRepository<Book> bookRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _humanRepository = humanRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 1.4.1.1 - Метод Get возвращающий список всех книг
        /// 1.2.2**.2 - возможность сделать запрос с сортировкой по автору, имени книги и жанру
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBooks([FromQuery] BookSpecParams bookSpecParams)
        {
            var spec = new BookSpecificationAuthorTitleGenre(bookSpecParams);
            var books = await _bookRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// 1.4.1.2 - Список всех книг по автору (фильтрация AuthorId)
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        [HttpGet("{AuthorId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthorId(int AuthorId)
        {
            var spec = new BookSpecificationAuthorId(AuthorId);
            var books = await _bookRepository.ListAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books));
        }

        /// <summary>
        /// 1.4.2 - Метод POST добавляющий новую книгу
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookDto dto)
        {
            var author = await _humanRepository.GetByIdAsync(dto.Author.Id);
            if (author == null) return BadRequest();
            var book = _mapper.Map<BookDto, Book>(dto);
            var result = await _bookRepository.AddAsync(book);
            return Ok(_mapper.Map<Book, BookDto>(result));
        }

        /// <summary>
        /// 1.4.3 - Метод DELETE удаляющий книгу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return BadRequest();
            await _bookRepository.DeleteAsync(book);
            return Ok();
        }
    }
}

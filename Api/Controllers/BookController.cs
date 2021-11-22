using Api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.QueryParams;
using Dto;
using Infrastructure.Data;
using Infrastructure.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext context;
        private readonly IGenericRepository<Book> _bookRepository;

        public BookController(ILogger<BookController> logger,
            IMapper mapper,
            AppDbContext context,
            IGenericRepository<Book> bookRepository)
        {
            _logger = logger;
            _mapper = mapper;
            this.context = context;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 1.4.1.1 - Метод Get возвращающий список всех книг
        /// 1.2.2**.2 - возможность сделать запрос с сортировкой по автору, имени книги и жанру
        /// 2.7.2.4.	Можно получить список всех книг с фильтром по автору 
        /// (По любой комбинации трёх полей сущности автор. Имеется  ввиду условие equals + and )
        /// 2.7.2.5.	Можно получить список книг по жанру. Книга + жанр + автор
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBooks([FromQuery] BookSpecParams bookSpecParams)
        {
            var user = context.Books.Include(i => i.Genres).FirstOrDefault();
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
        /// 2.7.2.1. - Книга может быть добавлена (POST) (вместе с автором и жанром) книга + автор + жанр
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookDto dto)
        {
            //var author = await _humanRepository.GetByIdAsync(dto.Author.Id);
            //if (author == null) return BadRequest();
            //var book = _mapper.Map<BookDto, Book>(dto);
            //var result = _bookRepository.Add(book);
            //return Ok(_mapper.Map<Book, BookDto>(result));
            return Ok();
        }

        /// <summary>
        /// 2.7.2.3. - Книге можно присвоить новый жанр, или удалить один из имеющихся 
        /// (PUT с телом.На вход сущность Book или её Dto) 
        /// При добавлении или удалении вы должны просто либо добавлять запись, 
        /// либо удалять из списка жанров. Каскадно удалять все жанры и книги с таким жанром нельзя! 
        /// Книга + жанр + автор
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<BookDto>> UpdateGenre(BookDto dto)
        {
            //var book = await _bookRepository.GetByIdAsync(dto.Id);
            //var genres = _mapper.Map<IEnumerable<GenreDto>, IEnumerable<Genre>>(dto.Genres);
            //var result = await _genreService.UpdateBookGenres(book, genres);
            //if (result) return BadRequest(Error.GetJsonError("Не удалось обновить жанры"));
            //var spec = new BookSpecificationGenre(dto.Id);
            //var bookUpdate = await _bookRepository.GetEntityWithSpec(spec);
            //return Ok(_mapper.Map<Book, BookDto>(bookUpdate));
            return Ok();
        }

        /// <summary>
        /// 2.7.2.2.	Книга может быть удалена из списка библиотеки (но только если она не у пользователя) по ID (ок, или ошибка, что книга у пользователя)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var spec = new BookSpecificationLibraryCard(id);
            var book = await _bookRepository.GetEntityWithSpec(spec);
            if (book == null) return BadRequest(Error.GetJsonError("Не удалось удалить книгу"));
            if (book.LibraryCards.Count != 0) return BadRequest(Error.GetJsonError("Книга у пользователя"));
            _bookRepository.Delete(book);
            return Ok();
        }
    }
}

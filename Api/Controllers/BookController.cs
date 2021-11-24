using Api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.QueryParams;
using Dto;
//using Infrastructure.Data;
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
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public BookController(ILogger<BookController> logger,
            IMapper mapper,
            IGenericRepository<Book> bookRepository,
            IAuthorService authorService,
            IGenreService genreService)
        {
            _logger = logger;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _authorService = authorService;
            _genreService = genreService;
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
        public async Task<IEnumerable<BookToReturnDto>> GetBooks([FromQuery] BookSpecParams bookSpecParams)
        {
            var spec = new BookSpecificationAuthorTitleGenre(bookSpecParams);
            var books = await _bookRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookToReturnDto>>(books);
        }

        /// <summary>
        /// 1.4.1.2 - Список всех книг по автору (фильтрация AuthorId)
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        [HttpGet("{AuthorId}")]
        public async Task<ActionResult<IEnumerable<BookToReturnDto>>> GetBooksByAuthorId(int AuthorId)
        {
            var spec = new BookSpecificationAuthorId(AuthorId);
            var books = await _bookRepository.ListAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookToReturnDto>>(books));
        }

        /// <summary>
        /// 2.7.2.1. - Книга может быть добавлена (POST) (вместе с автором и жанром) книга + автор + жанр
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BookToReturnDto>> CreateBook(BookToAddDto dto)
        {
            var book = _mapper.Map<BookToAddDto, Book>(dto);
            var result = _bookRepository.Add(book);
            if (!(await _bookRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось создать книгу"));
            return Ok(_mapper.Map<Book, BookToReturnDto>(result));
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
        public async Task<ActionResult<BookToReturnDto>> UpdateGenre(BookToUpdateDto dto)
        {
            var spec = new BookSpecification(dto.Id);
            var book = await _bookRepository.GetEntityWithSpec(spec);
            book.Genres = (await _genreService.UpdateGenres(book.Genres, dto.Genres.Select(s => s.GenreName))).ToList();
            var result = _bookRepository.Update(book);
            if (!(await _bookRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось обновить жанры у книги"));
            return Ok(_mapper.Map<Book, BookToReturnDto>(result));
        }

        /// <summary>
        /// 2.7.2.2.	Книга может быть удалена из списка библиотеки (но только если она не у пользователя) по ID (ок, или ошибка, что книга у пользователя)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var spec = new BookSpecification(id);
            var book = await _bookRepository.GetEntityWithSpec(spec);
            if (book == null) return BadRequest(new Error("Не удалось удалить книгу"));
            if (book.People.Count != 0) return BadRequest(new Error("Книга у пользователя"));
            _bookRepository.Delete(book);
            if (!(await _bookRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось удалить книгу"));
            return Ok();
        }
    }
}

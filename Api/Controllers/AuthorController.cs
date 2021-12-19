using Api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.QueryParams;
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
    /// 2.7.3 - Контроллер авторы
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookService _bookService;

        public AuthorController(ILogger<AuthorController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBookService bookService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookService = bookService;
        }

        /// <summary>
        /// 2.7.3.1.	Можно получить список всех авторов. (без книг, как и везде, где не указано обратное)
        /// 2.8.2.	Создать новый метод контроллера,  который будет выводить список всех авторов, 
        /// у которых есть хотя бы одна книга, написанная в определенный год(этот год прокидывать в query параметрах контроллера). 
        /// Отсортировать список авторов по алфавиту. 
        /// Предусмотреть возможность сортировки по возрастанию и по 
        /// убыванию(этот параметр также передавать через параметры контроллера)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AuthorToReturnDto>> GetAuthor([FromQuery] AuthorSpecParams specParams)
        {
            var spec = new AuthorSpecification(specParams);
            var authors = await _unitOfWork.Repository<Author>().ListAsync(spec);
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorToReturnDto>>(authors);
        }

        /// <summary>
        /// 2.7.3.2.	Можно получить список книг автора (книг может и не быть). автор + книги + жанры
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookToReturnDto>>> GetBooks(int id)
        {
            var spec = new AuthorSpecification(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpec(spec);
            if (author == null) return NotFound(new Error("Автор не найден"));
            var result = _mapper.Map<IEnumerable<Book>, IEnumerable<BookToReturnDto>>(author.Books);
            return Ok(result);
        }

        /// <summary>
        /// 2.8.3.	В этом же контроллере создать новый метод, который будет выводить всех авторов, 
        /// у которых название книги СОДЕРЖИТ указанную в параметрах подстроку 
        /// ("Война и мир" содержит "мир", регистр не учитывать)
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet("searchString")]
        public async Task<ActionResult<IEnumerable<AuthorToReturnDto>>> GetBooks(string searchString)
        {
            var spec = new BookSpecification(searchString);
            var books = await _unitOfWork.Repository<Book>().ListAsync(spec);
            var authors = books
                .Select(s => s.Author)
                .GroupBy(x => x.Id)
                .Select(s => s.FirstOrDefault());
            return Ok(_mapper.Map<IEnumerable<Author>, IEnumerable<AuthorToReturnDto>>(authors));
        }

        /// <summary>
        /// 2.7.3.3. - Добавить автора (с книгами или без) ответ - автор + книги
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorToReturnWithBookDto>> AddAuthor(AuthorToAddWithBookDto dto)
        {
            var author = _mapper.Map<AuthorToAddWithBookDto, Author>(dto);
            author.Books = (await _bookService.ResolveBookGenres(author.Books)).ToList();
            var addAuthor = _unitOfWork.Repository<Author>().Add(author);
            if (!(await _unitOfWork.SaveAsync())) return BadRequest(new Error("Не удалось добавить автора"));
            return Ok(_mapper.Map<Author, AuthorToReturnWithBookDto>(addAuthor));
        }

        /// <summary>
        /// 7.3.4.	Удалить автора (если только нет книг, иначе кидать ошибку с пояснением, 
        /// что нельзя удалить автора пока есть его книги) - Ок или Ошибка.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var spec = new AuthorSpecification(id);
            var author = await _unitOfWork.Repository<Author>().GetEntityWithSpec(spec);
            if (author.Books.Count != 0)
                return BadRequest(new Error("Не возможно удалита автора, у автора есть книги в базе"));
            _unitOfWork.Repository<Author>().Delete(author);
            if (!(await _unitOfWork.SaveAsync())) 
                return BadRequest(new Error("Не удалось удалить автора"));
            return Ok();
        }

    }
}

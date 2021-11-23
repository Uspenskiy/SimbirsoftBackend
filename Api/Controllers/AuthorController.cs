using Api.Helpers;
using AutoMapper;
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
    /// 2.7.3 - Контроллер авторы
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Author> _authotRepository;

        public AuthorController(ILogger<AuthorController> logger,
            IMapper mapper,
            IGenericRepository<Author> authotRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _authotRepository = authotRepository;
        }

        /// <summary>
        /// 2.7.3.1.	Можно получить список всех авторов. (без книг, как и везде, где не указано обратное)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAuthor()
        {
            var authors = await _authotRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// 2.7.3.2.	Можно получить список книг автора (книг может и не быть). автор + книги + жанры
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<BookDto>> GetBooks(int id)
        {
            var spec = new AuthorSpecification(id);
            var author = await _authotRepository.GetEntityWithSpec(spec);
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(author.Books);
        }

        /// <summary>
        /// 2.7.3.3. - Добавить автора (с книгами или без) ответ - автор + книги
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorWithBookDto>> AddAuthor(AuthorWithBookDto dto)
        {
            var author = _mapper.Map<AuthorWithBookDto, Author>(dto);
            var result = _authotRepository.Add(author);
            if (!(await _authotRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось добавить автора"));
            return Ok(_mapper.Map<Author, AuthorWithBookDto>(result));
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
            var author = await _authotRepository.GetEntityWithSpec(spec);
            if (author.Books.Count != 0)
                return BadRequest(new Error("Не возможно удалита автора, у автора есть книги в базе"));
            _authotRepository.Delete(author);
            if (!(await _authotRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось удалить автора"));
            return Ok();
        }

    }
}

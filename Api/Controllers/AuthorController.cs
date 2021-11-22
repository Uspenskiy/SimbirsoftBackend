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
    /// 2.7.3 - Контроллер авторы
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 2.7.3.1.	Можно получить список всех авторов. (без книг, как и везде, где не указано обратное)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAuthor()
        {
            return new List<AuthorDto>();
        }

        /// <summary>
        /// 2.7.3.2.	Можно получить список книг автора (книг может и не быть). автор + книги + жанры
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<BookDto>> GetBooks(int id)
        {
            return new List<BookDto>();
        }

        /// <summary>
        /// 2.7.3.3.	Добавить автора (с книгами или без) ответ - автор + книги
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthorDto> AddAuthor(AuthorDto dto)
        {
            return dto;
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
            return Ok();
        }

    }
}

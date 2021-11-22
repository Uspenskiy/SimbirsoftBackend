using Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 2.7.4. - Контроллер жанры:
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        public GenreController()
        {

        }

        /// <summary>
        /// 2.7.4.1. - Просмотр всех жанров. (без книг) 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            return new List<GenreDto>();
        }

        /// <summary>
        /// 2.7.4.3.	Вывод статистики Жанр - количество книг
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("Statistic/{id}")]
        public async Task<int> GetStatistic(GenreDto dto)
        {
            return 0;
        }

        /// <summary>
        /// 2.7.4.2. - Добавление нового жанра. (без книги)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenreDto> AddGenre(GenreDto dto)
        {
            return dto;
        }

    }
}

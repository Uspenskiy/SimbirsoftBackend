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
    /// 2.7.4. - Контроллер жанры:
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Genre> _repository;

        public GenreController(ILogger<GenreController> logger,
            IMapper mapper,
            IGenericRepository<Genre> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 2.7.4.1. - Просмотр всех жанров. (без книг) 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GenreToReturnDto>> GetGenres()
        {
            var ganres = await _repository.ListAllAsync();
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreToReturnDto>>(ganres);
        }

        /// <summary>
        /// 2.7.4.3.	Вывод статистики Жанр - количество книг
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("Statistic/{id}")]
        public async Task<int> GetStatistic(int id)
        {
            var spec = new GenreSpecification(id);
            var genere = await _repository.GetEntityWithSpec(spec);
            if (genere == null) return 0;
            return genere.Books.Count();
        }

        /// <summary>
        /// 2.7.4.2. - Добавление нового жанра. (без книги)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GenreToReturnDto>> AddGenre(String genreName)
        {
            var genere = new Genre { GenreName = genreName };
            var result = _repository.Add(genere);
            if (!(await _repository.SaveAsync()))
                return BadRequest(new Error("Не удалось добавить жанр"));
            return Ok(_mapper.Map<Genre, GenreToReturnDto>(result));
        }

    }
}

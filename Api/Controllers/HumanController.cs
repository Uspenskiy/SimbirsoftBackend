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
    /// 1.2.1 - Контроллер, который отвечает за человека.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HumanController : ControllerBase
    {
        private readonly ILogger<HumanController> _logger;
        private readonly IHumanRepository _humanRepository;
        private readonly IBookRepository _bookRepository;

        public HumanController(ILogger<HumanController> logger, 
            IHumanRepository humanRepository, 
            IBookRepository bookRepository)
        {
            _logger = logger;
            _humanRepository = humanRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 1.2.1 - Метод Get возвращающий список всех людей 
        /// или в имени, фамилии или отчестве которых содержится поисковая фраза searchParams.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<HumanDto>> GetHumans([FromQuery] string searchParams)
        {
            var result = await _humanRepository.ListAsync(searchParams != null ? searchParams : "");
            return result.Select(i => new HumanDto
            {
                Id = i.Id,
                Name = i.Name,
                Surname = i.Surname,
                Patronymic = i.Patronymic,
                Birthday = i.Birthday.ToShortDateString()
            });
        }

        /// <summary>
        /// 1.2.1 - Список людей, которые пишут книги.
        /// </summary>
        /// <returns></returns>
        [HttpGet("authors")]
        public async Task<IEnumerable<HumanDto>> GetAuthors()
        {
            var authors = await _bookRepository.ListAllAsync();
            return authors.Select(i => new HumanDto
            {
                Id = i.Author.Id,
                Name = i.Author.Name,
                Surname = i.Author.Surname,
                Patronymic = i.Author.Patronymic,
                Birthday = i.Author.Birthday.ToShortDateString()
            });
        }

        /// <summary>
        /// 1.2.1 - Метод POST добавляющий нового человека.
        /// </summary>
        /// <param name="human"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HumanDto> CreateHuman(HumanDto human)
        {
            await _humanRepository.AddAsync(new Human
            {
                Name = human.Name,
                Surname = human.Surname,
                Patronymic = human.Patronymic,
                Birthday = DateTime.Parse(human.Birthday)
            });
            return human;
        }

        /// <summary>
        /// Метод DELETE удаляющий человека.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHuman(int id)
        {
            var human = await _humanRepository.GetByIdAsync(id);
            await _humanRepository.DeleteAsync(human);
            return Ok();
        }
    }
}

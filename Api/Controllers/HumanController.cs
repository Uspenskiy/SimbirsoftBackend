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
    /// 1.3 - Контроллер, который отвечает за человека.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HumanController : ControllerBase
    {
        private readonly ILogger<HumanController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Human> _humanRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public HumanController(ILogger<HumanController> logger,
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
        /// 1.3.1.1 и 1.3.1.3 - Метод Get возвращающий список всех людей 
        /// или в имени, фамилии или отчестве которых содержится поисковая фраза searchParams.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<HumanDto>> GetHumans([FromQuery] string searchParams)
        {
            var spec = new HumanSpecificationNameSearch(searchParams);
            var humans = await _humanRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Human>, IEnumerable<HumanDto>>(humans);
        }

        /// <summary>
        /// 1.3.1.2 - Список людей, которые пишут книги.
        /// </summary>
        /// <returns></returns>
        [HttpGet("authors")]
        public async Task<IEnumerable<HumanDto>> GetAuthors()
        {
            var spec = new HumanSpecificationAuthors();
            var humans = await _humanRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Human>, IEnumerable<HumanDto>>(humans);
        }

        /// <summary>
        /// 1.3.2 - Метод POST добавляющий нового человека.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HumanDto> CreateHuman(HumanDto dto)
        {
            var human = _mapper.Map<HumanDto, Human>(dto);
            var result = await _humanRepository.AddAsync(human);
            return _mapper.Map<Human, HumanDto>(result);
        }

        /// <summary>
        /// 1.3.3 Метод DELETE удаляющий человека.
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

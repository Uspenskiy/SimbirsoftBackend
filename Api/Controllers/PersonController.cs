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
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 2.7.1 - Контроллер пользователя
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Person> _personRepository;
        private readonly IGenericRepository<Book> _bookRepository;

        public PersonController(ILogger<PersonController> logger,
            IMapper mapper, 
            IGenericRepository<Person> personRepository,
            IGenericRepository<Book> bookRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _personRepository = personRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 1.3.1.1 и 1.3.1.3 - Метод Get возвращающий список всех людей 
        /// или в имени, фамилии или отчестве которых содержится поисковая фраза searchParams.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonDto>> GetPersons([FromQuery] string searchParams)
        {
            var spec = new PersonSpecificationNameSearch(searchParams);
            var person = await _personRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(person);
        }

        /// <summary>
        /// 2.7.1.5. - Получить список всех взятых пользователем книг (GET) в качестве параметра поиска - ID пользователя. 
        /// Полное дерево: Книги - автор - жанр
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("TakenBooks/{id}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetTakenBook(int id)
        {
            var spec = new PersonSpecificationTakenBook(id);
            var person = await _personRepository.GetEntityWithSpec(spec);
            if (person == null) return NotFound(new Error("Не удалось создать пользователя"));
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(person.Books));
        }

        /// <summary>
        /// 1.3.1.2 - Список людей, которые пишут книги.
        /// </summary>
        /// <returns></returns>
        [HttpGet("authors")]
        public async Task<IEnumerable<PersonDto>> GetAuthors()
        {
            var spec = new HumanSpecificationAuthors();
            var humans = await _personRepository.ListAsync(spec);
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(humans);
        }

        /// <summary>
        /// 2.7.1.1. - Пользователь может быть добавлен. (POST) (вернуть пользователя)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson(PersonDto dto)
        {
            var person = _mapper.Map<PersonDto, Person>(dto);
            var entity = _personRepository.Add(person);
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось создать пользователя"));
            return _mapper.Map<Person, PersonDto>(entity);
        }

        /// <summary>
        /// 2.7.1.6. - Пользователь может взять книгу (добавить в список книг пользователя книгу)  Пользователь + книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("TakeBook")]
        public async Task<ActionResult<PersonWithBookDto>> TakeBook(LibraryCardDto dto)
        {
            var spec = new PersonSpecificationTakenBook(dto.Person.Id);
            var person = await _personRepository.GetEntityWithSpec(spec);
            var book = await _bookRepository.GetByIdAsync(dto.Book.Id);
            person.Books.Add(book);
            _personRepository.Update(person);
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось взять книгу"));
            return Ok(_mapper.Map<Person, PersonWithBookDto>(person));
        }

        /// <summary>
        /// 2.7.1.7. - Пользователь может вернуть книгу (удалить из списка книг пользователя книгу) пользователь + книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("ReturnBook")]
        public async Task<ActionResult<PersonWithBookDto>> ReturnBook(LibraryCardDto dto)
        {
            var spec = new PersonSpecificationTakenBook(dto.Person.Id);
            var person = await _personRepository.GetEntityWithSpec(spec);
            var book = await _bookRepository.GetByIdAsync(dto.Book.Id);
            person.Books.Remove(book);
            _personRepository.Update(person);
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось взять книгу"));
            return Ok(_mapper.Map<Person, PersonWithBookDto>(person));
        }

        /// <summary>
        /// 2.7.1.2. - Информация о пользователе может быть изменена (PUT) (вернуть пользователя)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<PersonDto>> UpdatePerson(PersonDto dto)
        {
            var person = await _personRepository.GetByIdAsync(dto.Id);
            if (person == null) 
                return NotFound(new Error("Пользователь отсутсвует"));
            _mapper.Map(dto, person);
            var entity = _personRepository.Update(person);
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось обновить пользователя"));
            return _mapper.Map<Person, PersonDto>(entity);
        }

        /// <summary>
        /// 2.7.1.3. - Пользователь может быть удалён по ID (DELETE) (ок или ошибку, если такого id нет)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return NotFound(new Error("Пользователь отсутсвует"));
            _personRepository.Delete(person);
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось удалить пользователя"));
            return Ok();
        }

        /// <summary>
        /// 2.7.1.4. - Пользователь или пользователи могут быть удалены по ФИО 
        /// (не заботясь о том что могут быть полные тёзки. Без пощады.) (DELETE) 
        /// Ok - или ошибку, если что-то пошло не так. 
        /// </summary>
        /// <param name="deletePersonSpecParams"></param>
        /// <returns></returns>
        [HttpDelete("DeleteByName")]
        public async Task<ActionResult> DeletePersonByName([FromQuery] DeletePersonSpecParams deletePersonSpecParams)
        {
            var spec = new PersonSpecificationNameSearch(deletePersonSpecParams);
            var people = await _personRepository.ListAsync(spec);
            if (people == null) return NotFound(new Error("Пользователь отсутсвует"));
            foreach (var person in people)
            {
                _personRepository.Delete(person);
            }
            if (!(await _personRepository.SaveAsync()))
                return BadRequest(new Error("Не удалось удалить пользователя"));
            return Ok();
        }
    }
}

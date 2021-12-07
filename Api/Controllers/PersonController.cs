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
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(ILogger<PersonController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 1.3.1.1 и 1.3.1.3 - Метод Get возвращающий список всех людей 
        /// или в имени, фамилии или отчестве которых содержится поисковая фраза searchParams.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonToReturnDto>> GetPersons([FromQuery] string searchParams)
        {
            var spec = new PersonSpecificationNameSearch(searchParams);
            var person = await _unitOfWork.Repository<Person>().ListAsync(spec);
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonToReturnDto>>(person);
        }

        /// <summary>
        /// 2.7.1.5. - Получить список всех взятых пользователем книг (GET) в качестве параметра поиска - ID пользователя. 
        /// Полное дерево: Книги - автор - жанр
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("TakenBooks/{id}")]
        public async Task<ActionResult<IEnumerable<BookToReturnDto>>> GetTakenBook(int id)
        {
            var spec = new PersonSpecificationTakenBook(id);
            var person = await _unitOfWork.Repository<Person>().GetEntityWithSpec(spec);
            if (person == null) return NotFound(new Error("Не удалось найти пользователя"));
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookToReturnDto>>(person.Books));
        }

        /// <summary>
        /// 1.3.1.2 - Список людей, которые пишут книги.
        /// </summary>
        /// <returns></returns>
        [HttpGet("authors")]
        public async Task<IEnumerable<AuthorToReturnDto>> GetAuthors()
        {
            var authors = await _unitOfWork.Repository<Author>().ListAllAsync();
            return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorToReturnDto>>(authors);
        }

        /// <summary>
        /// 2.7.1.1. - Пользователь может быть добавлен. (POST) (вернуть пользователя)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PersonToReturnDto>> CreatePerson(PersonToAddDto dto)
        {
            var person = _mapper.Map<PersonToAddDto, Person>(dto);
            var entity = _unitOfWork.Repository<Person>().Add(person);
            if (!(await _unitOfWork.SaveAsync()))
                return BadRequest(new Error("Не удалось создать пользователя"));
            return _mapper.Map<Person, PersonToReturnDto>(entity);
        }

        /// <summary>
        /// 2.7.1.6. - Пользователь может взять книгу (добавить в список книг пользователя книгу)  Пользователь + книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("TakeBook")]
        public async Task<ActionResult<PersonToReturnWithBookDto>> TakeBook(LibraryCardDto dto)
        {
            var spec = new PersonSpecificationTakenBook(dto.Person.Id);
            var person = await _unitOfWork.Repository<Person>().GetEntityWithSpec(spec);
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(dto.Book.Id);
            person.Books.Add(book);
            _unitOfWork.Repository<Person>().Update(person);
            if (!(await _unitOfWork.SaveAsync()))
                return BadRequest(new Error("Не удалось взять книгу"));
            return Ok(_mapper.Map<Person, PersonToReturnWithBookDto>(person));
        }

        /// <summary>
        /// 2.7.1.7. - Пользователь может вернуть книгу (удалить из списка книг пользователя книгу) пользователь + книги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("ReturnBook")]
        public async Task<ActionResult<PersonToReturnWithBookDto>> ReturnBook(LibraryCardDto dto)
        {
            var spec = new PersonSpecificationTakenBook(dto.Person.Id);
            var person = await _unitOfWork.Repository<Person>().GetEntityWithSpec(spec);
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(dto.Book.Id);
            person.Books.Remove(book);
            _unitOfWork.Repository<Person>().Update(person);
            if (!(await _unitOfWork.SaveAsync()))
                return BadRequest(new Error("Не удалось взять книгу"));
            return Ok(_mapper.Map<Person, PersonToReturnWithBookDto>(person));
        }

        /// <summary>
        /// 2.7.1.2. - Информация о пользователе может быть изменена (PUT) (вернуть пользователя)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<PersonToReturnDto>> UpdatePerson(PersonToUpdateDto dto)
        {
            var person = await _unitOfWork.Repository<Person>().GetByIdAsync(dto.Id);
            if (person == null) 
                return NotFound(new Error("Пользователь отсутсвует"));
            _mapper.Map(dto, person);
            var entity = _unitOfWork.Repository<Person>().Update(person);
            if (!(await _unitOfWork.SaveAsync()))
                return BadRequest(new Error("Не удалось обновить пользователя"));
            return _mapper.Map<Person, PersonToReturnDto>(entity);
        }

        /// <summary>
        /// 2.7.1.3. - Пользователь может быть удалён по ID (DELETE) (ок или ошибку, если такого id нет)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _unitOfWork.Repository<Person>().GetByIdAsync(id);
            if (person == null)
                return NotFound(new Error("Пользователь отсутсвует"));
            _unitOfWork.Repository<Person>().Delete(person);
            if (!(await _unitOfWork.SaveAsync()))
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
            var people = await _unitOfWork.Repository<Person>().ListAsync(spec);
            if (people == null) return NotFound(new Error("Пользователь отсутсвует"));
            foreach (var person in people)
            {
                _unitOfWork.Repository<Person>().Delete(person);
            }
            if (!(await _unitOfWork.SaveAsync()))
                return BadRequest(new Error("Не удалось удалить пользователя"));
            return Ok();
        }
    }
}

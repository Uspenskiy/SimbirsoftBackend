﻿using Core;
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
    /// 1.4 - Контроллер, который отвечает за книгу.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IHumanRepository _humanRepository;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger,
            IHumanRepository humanRepository,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _humanRepository = humanRepository;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 1.4.1.1 - Метод Get возвращающий список всех книг
        /// 1.2.2**.2 - возможность сделать запрос с сортировкой по автору, имени книги и жанру
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBooks([FromQuery] BookSpecParams bookSpecParams)
        {
            var books = await _bookRepository.ListAllAsync();
            if(bookSpecParams != null)
            {
                if (bookSpecParams.SortByAuthor)
                    books = books.OrderBy(s => s.Author.Surname)
                        .ThenBy(s => s.Author.Name)
                        .ThenBy(s => s.Author.Patronymic)
                        .ToList();
                if (bookSpecParams.SortByTitle)
                    books = books.OrderBy(s => s.Title)
                        .ToList();
                if (bookSpecParams.SortByGenre)
                    books = books.OrderBy(s => s.Genre)
                        .ToList();
            }
            return books.Select(i => new BookDto
            {
                Id = i.Id,
                Title = i.Title,
                Genre = i.Genre,
                Author = new HumanDto
                {
                    Id = i.Author.Id,
                    Name = i.Author.Name,
                    Surname = i.Author.Surname,
                    Patronymic = i.Author.Patronymic,
                    Birthday = i.Author.Birthday.ToShortDateString()
                }
            });
        }

        /// <summary>
        /// 1.4.1.2 - Список всех книг по автору (фильтрация AuthorId)
        /// </summary>
        /// <param name="AuthorId"></param>
        /// <returns></returns>
        [HttpGet("{AuthorId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthorId(int AuthorId)
        {
            var books = await _bookRepository.ListAllAsync();
            return Ok(books
                .Where(w => w.Author.Id == AuthorId)
                .Select(i => new BookDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Genre = i.Genre,
                    Author = new HumanDto
                    {
                        Id = i.Author.Id,
                        Name = i.Author.Name,
                        Surname = i.Author.Surname,
                        Patronymic = i.Author.Patronymic,
                        Birthday = i.Author.Birthday.ToShortDateString()
                    }
                }));
        }

        /// <summary>
        /// 1.4.2 - Метод POST добавляющий новую книгу
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookDto book)
        {
            var author = await _humanRepository.GetByIdAsync(book.Author.Id);
            if (author == null)
                return BadRequest();
            await _bookRepository.AddAsync(new Book 
            {
                Title = book.Title,
                Genre = book.Genre,
                Author = author
            }); 
            return Ok(book);
        }

        /// <summary>
        /// 1.4.3 - Метод DELETE удаляющий книгу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return BadRequest();
            await _bookRepository.DeleteAsync(book);
            return Ok();
        }
    }
}

using Api.Controllers;
using Api.Helpers;
using AutoFixture;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Dto;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimbirsoftBackend.Tests.Controllers
{
    public class AuthorControllerTests : BaseControllerTests
    {
        private Fixture _fixture;
        private ILogger<AuthorController> _logger;
        private Mock<IGenericRepository<Author>> _authorGenericRepository;
        private Mock<IGenericRepository<Book>> _bookGenericRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IBookService> _bookService;

        public AuthorControllerTests()
        {
            _fixture = new Fixture();
            var mock = new Mock<ILogger<AuthorController>>();
            _logger = mock.Object;
            _authorGenericRepository = new Mock<IGenericRepository<Author>>();
            _bookGenericRepository = new Mock<IGenericRepository<Book>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _bookService = new Mock<IBookService>();
        }

        [Fact]
        public async Task GetAuthor_WithNullSpecFromQuery_ShouldReturn_10()
        {
            var authors = _fixture.Build<Author>()
                .Without(w => w.Books)
                .CreateMany(10);

            _authorGenericRepository.Setup(r => r.ListAsync(It.IsAny<ISpecification<Author>>()))
                .ReturnsAsync(authors.ToList());
            _unitOfWork.Setup(s => s.Repository<Author>())
                 .Returns(_authorGenericRepository.Object);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.GetAuthor(null);

            Assert.NotNull(result);
            Assert.Equal(authors.Count(), result.Count());
        }

        [Fact]
        public async Task GetBooks_WithId_1_ShouldReturn_NotFound()
        {
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .With(w => w.Id, 2)
                .Create();
            var authors = new List<Author> { author };
            _authorGenericRepository.Setup(r => r.ListAsync(It.IsAny<ISpecification<Author>>()))
                 .ReturnsAsync(authors.ToList());
            _unitOfWork.Setup(s => s.Repository<Author>())
                 .Returns(_authorGenericRepository.Object);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.GetBooks(1);

            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBooks_WithId_1_ShouldReturn_ShouldReturn_Books()
        {
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .With(w => w.Id, 1)
                .Create();
            author.Books = _fixture.Build<Book>()
                .With(w => w.Author, author)
                .With(w => w.AuthorId, author.Id)
                .Without(w => w.BookGenres)
                .Without(w => w.LibraryCards)
                .Without(w => w.Genres)
                .Without(w => w.People)
                .CreateMany(10)
                .ToList();
            _authorGenericRepository.Setup(r => r.GetEntityWithSpec(It.IsAny<ISpecification<Author>>()))
                .ReturnsAsync(author);
            _unitOfWork.Setup(s => s.Repository<Author>())
                 .Returns(_authorGenericRepository.Object);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.GetBooks(1);
            var value = (result.Result as OkObjectResult).Value;

            Assert.NotNull(value);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(author.Books.Count, (value as IEnumerable<BookToReturnDto>).Count());
        }

        [Fact]
        public async Task GetBooks_WithSearchString_ShouldReturn_ShouldReturn_Author_1()
        {
            var searchString = "test";
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .With(w => w.Id, 1)
                .Create();
            var books= _fixture.Build<Book>()
                .With(w => w.Author, author)
                .With(w => w.AuthorId, author.Id)
                .With(w => w.Name, searchString)
                .Without(w => w.BookGenres)
                .Without(w => w.LibraryCards)
                .Without(w => w.Genres)
                .Without(w => w.People)
                .CreateMany(10)
                .ToList();
            _bookGenericRepository.Setup(r => r.ListAsync(It.IsAny<ISpecification<Book>>()))
                .ReturnsAsync(books);
            _unitOfWork.Setup(s => s.Repository<Book>())
                 .Returns(_bookGenericRepository.Object);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.GetBooks(author.FirstName);
            var value = (result.Result as OkObjectResult).Value;

            Assert.NotNull(value);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, (value as IEnumerable<AuthorToReturnDto>).Count());
        }

        [Fact]
        public async Task AddAuthor_ShouldReturn_Author()
        {
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .Create();
            author.Books = _fixture.Build<Book>()
                .With(w => w.Author, author)
                .With(w => w.AuthorId, author.Id)
                .Without(w => w.BookGenres)
                .Without(w => w.LibraryCards)
                .Without(w => w.Genres)
                .Without(w => w.People)
                .CreateMany(1)
                .ToList();
            _authorGenericRepository.Setup(r => r.Add(It.IsAny<Author>()))
                .Returns(author);
            _unitOfWork.Setup(s => s.Repository<Author>())
                 .Returns(_authorGenericRepository.Object);
            _unitOfWork.Setup(s => s.SaveAsync())
                .ReturnsAsync(true);

            var addAuthor = new AuthorToAddWithBookDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName,
                Books = author.Books.Select(b => new BookToAddDto
                {
                    Name = b.Name,
                    DateCreate = b.DateCreate,
                    Genres = b.Genres.Select(g => new GenreToAddDto { GenreName = g.GenreName})
                }).ToList()
            };

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.AddAuthor(addAuthor);
            var value = (result.Result as OkObjectResult).Value;

            Assert.NotNull(value);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(author.FirstName, (value as AuthorToReturnWithBookDto).FirstName);
            Assert.Equal(author.LastName, (value as AuthorToReturnWithBookDto).LastName);
            Assert.Equal(author.MiddleName, (value as AuthorToReturnWithBookDto).MiddleName);
            Assert.Equal(author.Books.Count, (value as AuthorToReturnWithBookDto).Books.Count());

        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturn_Ok()
        {
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .Create();
            _authorGenericRepository.Setup(r => r.Delete(It.IsAny<Author>()));
            _authorGenericRepository.Setup(r => r.GetEntityWithSpec(It.IsAny<ISpecification<Author>>()))
                .ReturnsAsync(author);
            _unitOfWork.Setup(s => s.Repository<Author>())
                .Returns(_authorGenericRepository.Object);
            _unitOfWork.Setup(s => s.SaveAsync())
                .ReturnsAsync(true);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.DeleteAuthor(author.Id);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldReturn_BadRequest()
        {
            var author = _fixture.Build<Author>()
                .Without(w => w.Books)
                .Create();
            author.Books = _fixture.Build<Book>()
                .With(w => w.Author, author)
                .With(w => w.AuthorId, author.Id)
                .Without(w => w.BookGenres)
                .Without(w => w.LibraryCards)
                .Without(w => w.Genres)
                .Without(w => w.People)
                .CreateMany(1)
                .ToList();
            _authorGenericRepository.Setup(r => r.Delete(It.IsAny<Author>()));
            _authorGenericRepository.Setup(r => r.GetEntityWithSpec(It.IsAny<ISpecification<Author>>()))
                .ReturnsAsync(author);
            _unitOfWork.Setup(s => s.Repository<Author>())
                .Returns(_authorGenericRepository.Object);
            _unitOfWork.Setup(s => s.SaveAsync())
                .ReturnsAsync(true);

            var controller = new AuthorController(_logger, _mapper, _unitOfWork.Object, _bookService.Object);
            var result = await controller.DeleteAuthor(author.Id);

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}

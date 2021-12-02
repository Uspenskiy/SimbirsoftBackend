using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Dto;
using Infrastructure.Specification;
using System.Collections.Generic;

namespace Api.Helpers
{
    public class BookNameResolver : IValueResolver<AuthorToAddWithBookDto, Author, List<Book>>
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Genre> _genreRepository;

        public BookNameResolver(IGenericRepository<Book> bookRepository,
            IGenericRepository<Genre> genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        public List<Book> Resolve(AuthorToAddWithBookDto source, Author destination, List<Book> destMember, ResolutionContext context)
        {
            var books = new List<Book>();
            foreach (var book in source.Books)
            {
                var genres = new List<Genre>();
                foreach (var genre in book.Genres)
                {
                    var spec = new GenreSpecification(genre.GenreName);
                    var entity = _genreRepository.GetEntityWithSpec(spec).Result;
                    if (entity == null)
                    {
                        entity = _genreRepository.Add(new Genre { GenreName = genre.GenreName });
                        _genreRepository.SaveAsync().Wait();
                    }
                    genres.Add(entity);
                }
                books.Add(_bookRepository.Add(new Book { Genres = genres, Name = book.Name }));
            }
            _bookRepository.SaveAsync().Wait();
            return books;
        }
    }
}
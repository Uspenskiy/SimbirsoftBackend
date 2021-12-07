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
        private readonly IUnitOfWork _unitOfWork;

        public BookNameResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    var entity = _unitOfWork.Repository<Genre>().GetEntityWithSpec(spec).Result;
                    if (entity == null)
                    {
                        entity = _unitOfWork.Repository<Genre>().Add(new Genre { GenreName = genre.GenreName });
                        _unitOfWork.SaveAsync().Wait();
                    }
                    genres.Add(entity);
                }
                books.Add(_unitOfWork.Repository<Book>().Add(new Book { Genres = genres, Name = book.Name }));
            }
            _unitOfWork.SaveAsync().Wait();
            return books;
        }
    }
}
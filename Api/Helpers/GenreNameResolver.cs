using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Dto;
using Infrastructure.Specification;
using System.Collections.Generic;

namespace Api.Helpers
{
    public class GenreNameResolver : IValueResolver<BookToAddDto, Book, List<Genre>>
    {
        private readonly IGenericRepository<Genre> _genreRepository;

        public GenreNameResolver(IGenericRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<Genre> Resolve(BookToAddDto source, Book destination, List<Genre> destMember, ResolutionContext context)
        {
            var genres = new List<Genre>();
            foreach (var genre in source.Genres)
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
            return genres;
        }
    }
}
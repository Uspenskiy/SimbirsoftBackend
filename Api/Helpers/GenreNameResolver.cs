using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Dto;
using Infrastructure.Specification;
using System.Collections.Generic;

namespace Api.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class GenreNameResolver : IValueResolver<BookToAddDto, Book, List<Genre>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreNameResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Genre> Resolve(BookToAddDto source, Book destination, List<Genre> destMember, ResolutionContext context)
        {
            var genres = new List<Genre>();
            foreach (var genre in source.Genres)
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
            return genres;
        }
    }
}
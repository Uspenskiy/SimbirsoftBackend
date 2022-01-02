using AutoMapper;
using Core.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    /// <summary>
    /// Описывает правила проекции сущностей и dto
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorToReturnDto>();

            CreateMap<AuthorToAddWithBookDto, Author>();

            CreateMap<Author, AuthorToReturnWithBookDto>();

            CreateMap<Book, BookToReturnDto>();

            CreateMap<BookToAddDto, Book>();

            CreateMap<Genre, GenreToReturnDto>();
            CreateMap<GenreToAddDto, Genre>();
            CreateMap<GenreToUpdateDto, Genre>();

            CreateMap<Person, PersonToReturnDto>();
            CreateMap<PersonToAddDto, Person>();
            CreateMap<PersonToUpdateDto, Person>();

            CreateMap<Person, PersonToReturnWithBookDto>();
        }
    }
}

using AutoMapper;
using Core.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorToReturnDto>();

            CreateMap<AuthorToAddWithBookDto, Author>()
                .ForMember(d => d.Books, o => o.MapFrom<BookNameResolver>());

            CreateMap<Author, AuthorToReturnWithBookDto>();

            CreateMap<Book, BookToReturnDto>();

            CreateMap<BookToAddDto, Book>()
                .ForMember(d => d.Genres, o => o.MapFrom<GenreNameResolver>());

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

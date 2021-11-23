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
            CreateMap<Book, BookDto>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
            CreateMap<BookDto, Book>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Title));

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<LibraryCard, LibraryCardDto>();
            CreateMap<LibraryCardDto, LibraryCard>();

            CreateMap<PersonDto, Person>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Surname))
                .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.Patronymic))
                .ForMember(d => d.BirthDate, o => o.MapFrom(s => s.Birthday));
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Patronymic, o => o.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Birthday, o => o.MapFrom(s => s.BirthDate));
            CreateMap<Person, PersonWithBookDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.Patronymic, o => o.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Birthday, o => o.MapFrom(s => s.BirthDate));
        }
    }
}

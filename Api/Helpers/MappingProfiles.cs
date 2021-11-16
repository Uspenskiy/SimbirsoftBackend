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
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Human, HumanDto>();
            CreateMap<HumanDto, Human>();

            CreateMap<LibraryCard, LibraryCardDto>();
            CreateMap<LibraryCardDto, LibraryCard>();
        }
    }
}

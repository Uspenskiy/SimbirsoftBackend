using Api.Controllers;
using Api.Helpers;
using AutoMapper;
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
    public class AuthorControllerTests
    {
        private static IMapper _mapper;

        public AuthorControllerTests()
        {
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(c => 
                {
                    c.AddProfile(new MappingProfiles());
                });
                _mapper = mappingConfig.CreateMapper();
            }
        }

        [Fact]
        public void GetAuthor_WithNullSpecFromQuery()
        {

        }
    }
}

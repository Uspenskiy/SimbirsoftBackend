using Api.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsoftBackend.Tests.Controllers
{
    public abstract class BaseControllerTests
    {
        protected static IMapper _mapper;

        public BaseControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(c =>
                {
                    c.AddProfile(new MappingProfiles());
                });
                _mapper = mappingConfig.CreateMapper();
            }
        }
    }
}

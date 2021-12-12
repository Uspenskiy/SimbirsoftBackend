using Api.Controllers;
using Api.Helpers;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
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
    public class AuthorControllerTests : BaseControllerTests
    {

        private static ILogger<AuthorController> _logger;

        public AuthorControllerTests()
        {
            if(_logger == null)
            {
                var mock = new Mock<ILogger<AuthorController>>();
                _logger = mock.Object;
            }
        }

        [Fact]
        public async Task GetAuthor_WithNullSpecFromQuery_ShouldReturn_1()
        {
            //var mockUnitOfWork = new Mock<IUnitOfWork>();

            //var unitOfWork = new UnitOfWork();
            //var controller = new AuthorController(_logger, _mapper, mockUnitOfWork.Object, mockBookService.Object);
            //var result = await controller.GetAuthor(null);

        }
    }
}

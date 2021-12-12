using Core.Entities;
using SimbirsoftBackend.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimbirsoftBackend.Tests.Services
{
    public class UnitOfWorkTests : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public UnitOfWorkTests(UnitOfWorkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetRepository_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            Assert.NotNull(unitOfWork);
        }

        [Fact]
        public async Task GetById_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<BaseEntity>();
            var entity = await repos.GetByIdAsync(1);
            Assert.NotNull(entity);
        }
    }
}

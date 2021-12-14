using Core.Entities;
using Infrastructure.Specification;
using SimbirsoftBackend.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimbirsoftBackend.Tests.Services
{
    public class AuthorRepositoryTest
        : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public AuthorRepositoryTest(UnitOfWorkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public void GetRepository_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            Assert.NotNull(unitOfWork);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task GetById_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entity = await repos.GetByIdAsync(1);
            Assert.NotNull(entity);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task GetBySpec_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entity = await repos.GetEntityWithSpec(new AuthorSpecification(1));
            Assert.NotNull(entity);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task ListAllAsync_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entitys = await repos.ListAllAsync();
            Assert.NotNull(entitys);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task ListAllAsync_SholdReturn_Count_1()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entitys = await repos.ListAllAsync();
            Assert.Equal(1, entitys.Count);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task ListAsync_SholdReturn_NotNull()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entitys = await repos.ListAsync(new AuthorSpecification(1));
            Assert.NotNull(entitys);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task ListAsync_SholdReturn_Count_0()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entitys = await repos.ListAsync(new AuthorSpecification(2));
            Assert.Equal(0, entitys.Count);
        }

        [Fact]
        [Trait("AuthorRepositoryTest", "UnitOfWorkTesting")]
        public async Task ListAsync_SholdReturn_Count_1()
        {
            var unitOfWork = _fixture.Create();
            var repos = unitOfWork.Repository<Author>();
            var entitys = await repos.ListAsync(new AuthorSpecification(1));
            Assert.Equal(1, entitys.Count);
        }
    }
}

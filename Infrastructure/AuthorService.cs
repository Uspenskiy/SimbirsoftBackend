using Core.Entities;
using Core.Interfaces;
using Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository<Author> _repository;

        public AuthorService(IGenericRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<Author> GetAuthor(string firstName, string lastName, string middleName)
        {
            var spec = new AuthorSpecification(firstName, lastName, middleName);
            var author = await _repository.GetEntityWithSpec(spec);
            if (author != null)
                return author;
            _repository.Add(new Author { FirstName = firstName, LastName = lastName, MiddleName = middleName });
            await _repository.SaveAsync();
            return await GetAuthor(firstName, lastName, middleName);
        }
    }
}

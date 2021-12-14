using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsoftBackend.Tests.Helpers
{
    public static class AuthorHelper
    {
        public static Author GetOne(int id = 1, int bookId = 1)
        {
            return new Author
            {
                Id = id,
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Books = new List<Book> { new Book { Id = bookId } },
                CreateEntityTime = new DateTimeOffset(1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                UpdateEntityTime = DateTimeOffset.Now,
                Version = 1
            };
        }

        internal static IEnumerable<Author> GetMany()
        {
            yield return GetOne();
        }
    }
}

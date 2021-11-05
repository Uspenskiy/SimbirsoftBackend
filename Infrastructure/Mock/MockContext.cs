using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mock
{
    /// <summary>
    /// 1.2.1 - Статичный список людей и книг (штуки по 3) каждой сущности.
    /// </summary>
    public static class MockContext
    {
        static MockContext()
        {
            Humens = new List<Human>
            {
                new Human { Id = 0, Name = "", Surname = "", Patronymic = "", Birthday = DateTime.Now },
                new Human { Id = 1, Name = "", Surname = "", Patronymic = "", Birthday = DateTime.Now },
                new Human { Id = 2, Name = "", Surname = "", Patronymic = "", Birthday = DateTime.Now }
            };
            Books = new List<Book>
            {
                new Book { Id = 0, Author = Humens[0], Title = "", Genre = "" },
                new Book { Id = 1, Author = Humens[0], Title = "", Genre = "" },
                new Book { Id = 2, Author = Humens[1], Title = "", Genre = "" }
            };
        }

        /// <summary>
        /// 1.2.1 - Статичный список людей
        /// </summary>
        public static List<Human> Humens { get; set; }

        /// <summary>
        /// 1.2.1 - Статичный список книг
        /// </summary>
        public static List<Book> Books { get; set; }
    }
}

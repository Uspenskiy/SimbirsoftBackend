using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mock
{
    /// <summary>
    /// 1.2.3 - Статичный список людей и книг (штуки по 3) каждой сущности.
    /// </summary>
    public static class MockContext
    {
        static MockContext()
        {
            Humens = new List<Human>
            {
                new Human { Id = 0, Name = "Лев", Surname = "Толстой", Patronymic = "Николаевич", Birthday = new DateTime(1828, 8, 28)  },
                new Human { Id = 1, Name = "Александр", Surname = "Пушкин", Patronymic = "Сергеевич", Birthday = new DateTime(1799, 5, 26) },
                new Human { Id = 2, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович", Birthday = new DateTime(1970, 1, 1) }
            };
            Books = new List<Book>
            {
                new Book { Id = 0, Author = Humens[0], Title = "Война и мир", Genre = "Роман" },
                new Book { Id = 1, Author = Humens[0], Title = "Анна Каренина", Genre = "Роман" },
                new Book { Id = 2, Author = Humens[1], Title = "Евгений Онегин", Genre = "Роман" }
            };
            LibraryCards = new List<LibraryCard>();
        }

        /// <summary>
        /// 1.2.3 - Статичный список людей
        /// </summary>
        public static List<Human> Humens { get; set; }

        /// <summary>
        /// 1.2.3 - Статичный список книг
        /// </summary>
        public static List<Book> Books { get; set; }

        /// <summary>
        /// 1.2.1*.3 - cтатичный список, отвечающий за хранение этих сущностей LibraryCard
        /// </summary>
        public static List<LibraryCard> LibraryCards { get; set; }
    }
}

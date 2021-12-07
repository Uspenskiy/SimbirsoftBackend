using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// 2.2 -Сущность отвечающая за пользователя
    /// </summary>
    public partial class Person : BaseEntity
    {
        public Person()
        {
            Books = new List<Book>();
        }

        public DateTimeOffset? BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<Book> Books { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }
    }
}

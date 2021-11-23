using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Person : BaseEntity
    {
        public DateTimeOffset? BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<Book> Books { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }
    }
}

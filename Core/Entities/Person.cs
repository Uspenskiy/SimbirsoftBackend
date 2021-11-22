using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Person : BaseEntity
    {
        public DateTime? BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public IReadOnlyList<Book> Books { get; set; }
        public IReadOnlyList<LibraryCard> LibraryCards { get; set; }
    }
}

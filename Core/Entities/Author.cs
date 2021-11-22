using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public virtual IReadOnlyList<Book> Books { get; set; }
    }
}

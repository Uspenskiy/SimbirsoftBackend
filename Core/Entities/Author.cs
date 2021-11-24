using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Author : BaseTimeEntity
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}

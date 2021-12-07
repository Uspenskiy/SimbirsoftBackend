using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// 2.2 -Сущность отвечающая за автора
    /// </summary>
    public partial class Author : BaseEntity
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

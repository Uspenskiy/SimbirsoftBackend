using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// 2.2 -Сущность отвечающая за жанр
    /// </summary>
    public partial class Genre : BaseEntity
    {
        public Genre()
        {
            Books = new List<Book>();
        }

        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}

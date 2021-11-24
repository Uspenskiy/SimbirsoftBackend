using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Genre : BaseTimeEntity
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

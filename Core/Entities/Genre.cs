using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public IReadOnlyList<Book> Books { get; set; }
        public IReadOnlyList<BookGenre> BookGenres { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public IReadOnlyList<BookGenre> BookGenres { get; set; }
    }
}

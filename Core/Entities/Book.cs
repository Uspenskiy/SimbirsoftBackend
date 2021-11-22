using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Book : BaseEntity
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public IReadOnlyList<BookGenre> BookGenres { get; set; }
        public IReadOnlyList<LibraryCard> LibraryCards { get; set; }
    }
}

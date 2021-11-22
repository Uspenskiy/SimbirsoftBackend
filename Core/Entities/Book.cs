using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Book : BaseEntity
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public IReadOnlyList<Genre> Genres { get; set; }
        public IReadOnlyList<Person> People { get; set; }
        public virtual IReadOnlyList<BookGenre> BookGenres { get; set; }
        public virtual IReadOnlyList<LibraryCard> LibraryCards { get; set; }
    }
}

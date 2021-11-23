using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Book : BaseEntity
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Person> People { get; set; }
        public List<BookGenre> BookGenres { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }
    }
}

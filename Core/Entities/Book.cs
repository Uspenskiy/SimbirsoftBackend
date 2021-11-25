using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// 2.2 -Сущность отвечающая за книгу
    /// </summary>
    public partial class Book : BaseTimeEntity
    {
        public Book()
        {
            Genres = new List<Genre>();
            People = new List<Person>();
        }

        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Person> People { get; set; }
        public List<BookGenre> BookGenres { get; set; }
        public List<LibraryCard> LibraryCards { get; set; }
        /// <summary>
        /// 2.8.1.	Расширить модель книги и добавить туда дату написания этой книги
        /// </summary>
        public DateTimeOffset DateCreate { get; set; }
    }
}

using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.QueryParams
{
    /// <summary>
    /// 1.2.2** - класс отвечающий за сортировку по автору, имени книги и жанру
    /// </summary>
    public class BookSpecParams
    {
        /// <summary>
        /// Сортировка по автору
        /// </summary>
        public bool SortByAuthor { get; set; }

        /// <summary>
        /// Сортировка по названию
        /// </summary>
        public bool SortByTitle { get; set; }

        /// <summary>
        /// Сортировка по жанру
        /// </summary>
        public bool SortByGenre { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public AuthorToReturnDto Author { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public GenreToReturnDto Genre { get; set; }
    }
}

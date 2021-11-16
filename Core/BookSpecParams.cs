using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// 1.2.2** - класс отвечающий за сортировку по автору, имени книги и жанру
    /// </summary>
    public class BookSpecParams
    {
        public bool SortByAuthor { get; set; }

        public bool SortByTitle { get; set; }

        public bool SortByGenre { get; set; }
    }
}

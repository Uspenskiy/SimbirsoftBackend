using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// Класс презентор добавления книги с жанрами
    /// </summary>
    public class BookToAddDto
    {
        public string Name { get; set; }

        public DateTimeOffset DateCreate { get; set; }

        public IEnumerable<GenreToAddDto> Genres { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// Класс презентер для обновления жанра книги
    /// </summary>
    public class GenreToUpdateDto
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}

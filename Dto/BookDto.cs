using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// 1.2.2 - Класс презентор Book
    /// </summary>
    public class BookDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите название книги")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Укажите жанры книги")]
        public IEnumerable<GenreDto> Genres { get; set; }
        [Required(ErrorMessage = "Укажите автора книги")]
        public AuthorDto Author { get; set; }
    }
}

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
    public class BookToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GenreToReturnDto> Genres { get; set; }
        public AuthorToReturnDto Author { get; set; }
        public DateTimeOffset DateCreate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// Класс презентор для добавления автора + книг
    /// </summary>
    public class AuthorToAddWithBookDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public virtual List<BookToAddDto> Books { get; set; }
    }
}

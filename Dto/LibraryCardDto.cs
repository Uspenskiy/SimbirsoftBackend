using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// 1.2.1*.1 - сущность-агрегатор (LibraryCard): человек, взявший для прочтения книгу
    /// </summary>
    public class LibraryCardDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите человека взявшего книгу")]
        public HumanDto Person { get; set; }
        [Required(ErrorMessage = "Укажите взятую книгу")]
        public BookDto Book { get; set; }
        public DateTimeOffset DateTimeCreate { get; set; }
    }
}

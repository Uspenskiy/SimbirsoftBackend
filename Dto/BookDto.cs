using System;
using System.Collections.Generic;
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
        public string Title { get; set; }
        public string Genre { get; set; }
        public HumanDto Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class PersonWithBookDto : PersonDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }
}

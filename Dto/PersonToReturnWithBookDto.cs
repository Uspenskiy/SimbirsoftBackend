using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class PersonToReturnWithBookDto : PersonToReturnDto
    {
        public IEnumerable<BookToReturnDto> Books { get; set; }
    }
}

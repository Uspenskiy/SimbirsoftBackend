using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// Класс презентер для возврата пользователя с книгами
    /// </summary>
    public class PersonToReturnWithBookDto : PersonToReturnDto
    {
        public IEnumerable<BookToReturnDto> Books { get; set; }
    }
}

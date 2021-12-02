using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    /// <summary>
    /// 1.2.1 - Класс презентор Human
    /// Класс презентер для возврата пользователя
    /// </summary>
    public class PersonToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTimeOffset Birthday { get; set; }
    }
}

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
    /// </summary>
    public class PersonToReturnDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите имя человека")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите фамилию человека")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Укажите отчество человека")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Укажите дату рождения человека")]
        public DateTimeOffset Birthday { get; set; }
    }
}

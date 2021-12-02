using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Helpers
{
    /// <summary>
    /// Класс для возврата сообшений об ошибке
    /// </summary>
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}

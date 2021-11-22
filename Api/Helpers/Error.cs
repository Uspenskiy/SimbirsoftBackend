using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class Error
    {
        public static string GetJsonError(string message)
        {
            return JsonSerializer.Serialize(new { ErrorMessage = message });
        }
    }
}

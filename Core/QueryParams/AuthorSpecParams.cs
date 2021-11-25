using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.QueryParams
{
    public class AuthorSpecParams
    {
        public DateTime Date { get; set; }
        public bool IsOrder { get; set; }
        public bool IsOrderByDescending { get; set; }
    }
}

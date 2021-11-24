using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseTimeEntity : BaseEntity
    {
        public DateTimeOffset CreateEntityTime { get; set; }

        public DateTimeOffset UpdateEntityTime { get; set; }
    }
}

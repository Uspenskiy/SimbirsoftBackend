using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// 9.1.	Расширить все сущности, кроме референсных дополнительными полями
    /// 9.1.1.	Дата и время + таймзона вставки записи.
    /// 9.1.2.	Дата и время + таймзона изменения записи.
    /// 9.1.3.	Версия записи.
    /// </summary>
    public class BaseTimeEntity : BaseEntity
    {
        public DateTimeOffset CreateEntityTime { get; set; }

        public DateTimeOffset UpdateEntityTime { get; set; }

        public int Version { get; set; }
    }
}

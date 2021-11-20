using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    /// <summary>
    /// 1.2.1*.1 - сущность-агрегатор (LibraryCard): человек, взявший для прочтения книгу
    /// </summary>
    public class LibraryCard
    {
        public int Id { get; set; }
        public Human Person { get; set; }
        public Book Book { get; set; }
        public DateTimeOffset DateTimeCreate { get; set; }
    }
}

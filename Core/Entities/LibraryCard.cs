using System;
using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// 2.2 - Сущность отвечающая за связь книги и пользователя
    /// </summary>
    public partial class LibraryCard
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Person Person { get; set; }
    }
}

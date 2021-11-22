using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class LibraryCard
    {
        public int BookId { get; set; }
        public int PersonId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Person Person { get; set; }
    }
}

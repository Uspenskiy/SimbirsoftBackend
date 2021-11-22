﻿using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class BookGenre
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Genre Genre { get; set; }
    }
}

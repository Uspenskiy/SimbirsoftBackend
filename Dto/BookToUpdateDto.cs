﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class BookToUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GenreToUpdateDto> Genres { get; set; }
    }
}

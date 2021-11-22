﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class BookSpecificationLibraryCard : BaseSpecification<Book>
    {
        public BookSpecificationLibraryCard(int id)
            : base(x => x.Id == id)
        {
            AddInclude(i => i.LibraryCards);
        }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class BookSpecification : BaseSpecification<Book>
    {
        public BookSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(i => i.Author);
            AddInclude(i => i.Genres);
            AddInclude(i => i.People);
        }
    }
}

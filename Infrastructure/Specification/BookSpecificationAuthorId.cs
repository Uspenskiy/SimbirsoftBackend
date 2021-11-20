using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class BookSpecificationAuthorId : BaseSpecification<Book>
    {
        public BookSpecificationAuthorId(int authorId) : base
            (x => x.Author.Id == authorId)
        {}
    }
}

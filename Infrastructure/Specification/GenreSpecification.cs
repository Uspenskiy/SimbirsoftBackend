using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class GenreSpecification : BaseSpecification<Genre>
    {
        public GenreSpecification(string genreName)
            : base(x => x.GenreName.ToLower() == genreName)
        { }

        public GenreSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(i => i.Books);
        }
    }
}

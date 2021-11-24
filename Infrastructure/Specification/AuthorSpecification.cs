using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class AuthorSpecification : BaseSpecification<Author>
    {
        public AuthorSpecification(string firstName, string lastName, string middleName)
            : base(x => x.FirstName.ToLower() == firstName.ToLower() &&
            x.LastName.ToLower() == lastName.ToLower() &&
            x.MiddleName.ToLower() == middleName.ToLower())
        {

        }

        public AuthorSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(i => i.Books);
        }

        public AuthorSpecification(AuthorSpecParams specParams)
            : base(x => x.Books.Count > 0)
        {
            AddInclude(i => i.Books);
        }
    }
}

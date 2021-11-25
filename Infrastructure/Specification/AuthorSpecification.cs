using Core.Entities;
using Core.QueryParams;
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
        {
            AddInclude(i => i.Books);
            if (specParams != null)
            {
                if(specParams.Date != null)
                    AddWhere(w => w.Books.FirstOrDefault(b => b.DateCreate.Year == specParams.Date.Year) != null);
                if(specParams.IsOrder)
                {
                    if(specParams.IsOrderByDescending)
                    {
                        AddOrderByDescending(o => o.FirstName);
                        AddOrderByDescending(o => o.LastName);
                        AddOrderByDescending(o => o.MiddleName);
                    }
                    else
                    {
                        AddOrderBy(o => o.FirstName);
                        AddOrderBy(o => o.LastName);
                        AddOrderBy(o => o.MiddleName);
                    }
                }
            }
        }
    }
}

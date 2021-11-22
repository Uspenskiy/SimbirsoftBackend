using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class PersonSpecificationTakenBook : BaseSpecification<Person>
    {

        public PersonSpecificationTakenBook(int id)
            : base(x => x.Id == id)
        {
            AddInclude(i => i.LibraryCards);
        }
    }
}

using Core.Entities;
using Core.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class PersonSpecificationNameSearch : BaseSpecification<Person>
    {
        public PersonSpecificationNameSearch(string nameSearch)
            : base(x => x.FirstName.Contains(nameSearch, StringComparison.OrdinalIgnoreCase) ||
            x.LastName.Contains(nameSearch, StringComparison.InvariantCultureIgnoreCase) || 
            x.MiddleName.Contains(nameSearch, StringComparison.InvariantCultureIgnoreCase))
        { }

        public PersonSpecificationNameSearch(DeletePersonSpecParams deletePersonSpecParams)
            : base(x => x.FirstName.ToLower() == deletePersonSpecParams.Name.ToLower() && 
            x.LastName.ToLower() == deletePersonSpecParams.Surname.ToLower() &&
            x.MiddleName.ToLower() == deletePersonSpecParams.Patronymic.ToLower())
        { }
    }
}

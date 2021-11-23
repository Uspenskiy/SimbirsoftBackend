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
        public PersonSpecificationNameSearch()
        { }

        public PersonSpecificationNameSearch(string nameSearch)
            : base(x => String.IsNullOrEmpty(nameSearch) || 
            x.FirstName.ToLower().Contains(nameSearch.ToLower()) || 
            x.LastName.ToLower().Contains(nameSearch.ToLower()) ||
            x.MiddleName.ToLower().Contains(nameSearch.ToLower()))
        { }

        public PersonSpecificationNameSearch(DeletePersonSpecParams deletePersonSpecParams)
            : base(x => x.FirstName.ToLower() == deletePersonSpecParams.Name.ToLower() && 
            x.LastName.ToLower() == deletePersonSpecParams.Surname.ToLower() &&
            x.MiddleName.ToLower() == deletePersonSpecParams.Patronymic.ToLower())
        { }
    }
}

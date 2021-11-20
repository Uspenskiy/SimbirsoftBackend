using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }
        
        public BaseSpecification(Func<T, bool> criteria)
        {
            Criteria = criteria;
        }

        public Func<T, bool> Criteria { get; private set; }

        public List<Func<T, object>> OrderBy { get; private set; }

        protected void AddOrderBy(Func<T, object> orderBy)
        {
            if (OrderBy == null)
                OrderBy = new List<Func<T, object>>();
            OrderBy.Add(orderBy);
        }
    }
}

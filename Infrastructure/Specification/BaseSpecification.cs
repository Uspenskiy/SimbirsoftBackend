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
        
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> OrderBy { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            if (OrderBy == null)
                OrderBy = new List<Expression<Func<T, object>>>();
            OrderBy.Add(orderBy);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            if (Includes == null)
                Includes = new List<Expression<Func<T, object>>>();
            Includes.Add(includeExpression);
        }
    }
}

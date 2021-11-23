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
            if (Criteriaes == null)
                Criteriaes = new List<Expression<Func<T, bool>>>();
            Criteriaes.Add(criteria);
        }

        public List<Expression<Func<T, bool>>> Criteriaes { get; private set; }

        public List<Expression<Func<T, object>>> OrderBy { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; private set; }

        public List<string> IncludeStrings { get; private set; }

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

        protected void AddInclude(string includeExpression)
        {
            if (IncludeStrings == null)
                IncludeStrings = new List<string>();
            IncludeStrings.Add(includeExpression);
        }

        protected void AddWhere(Expression<Func<T, bool>> criteria)
        {
            if (Criteriaes == null)
                Criteriaes = new List<Expression<Func<T, bool>>>();
            Criteriaes.Add(criteria);
        }
    }
}

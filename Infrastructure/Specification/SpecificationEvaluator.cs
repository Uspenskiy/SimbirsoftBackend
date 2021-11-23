using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteriaes != null)
            {
                foreach (var criteria in spec.Criteriaes)
                {
                    query = query.Where(criteria);
                }
            }

            if (spec.OrderBy != null)
            {
                var orderQuery = query.OrderBy(spec.OrderBy.First());
                if (spec.OrderBy.Count > 1)
                {
                    foreach (var thenBy in spec.OrderBy.Skip(1))
                        orderQuery = orderQuery.ThenBy(thenBy);
                }
                query = orderQuery;
            }
            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if(spec.IncludeStrings != null)
            {
                query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}

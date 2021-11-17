using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SpecificationEvaluator<T>
    {
        public static IEnumerable<T> Apply(IEnumerable<T> value, ISpecification<T> spec)
        {
            var result = value;
            if (spec.Criteria != null)
                result = result.Where(spec.Criteria);

            if(spec.OrderBy != null)
            {
                var buff = result.OrderBy(spec.OrderBy.First());
                if(spec.OrderBy.Count > 1)
                {
                    foreach (var thenBy in spec.OrderBy.Skip(1))
                        buff = buff.ThenBy(thenBy);
                }
                result = buff;
            }
            return result;
        }
    }
}

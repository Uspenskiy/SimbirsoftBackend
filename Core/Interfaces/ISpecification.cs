using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        Func<T, bool> Criteria { get; }
        List<Func<T, object>> OrderBy { get; }
    }
}

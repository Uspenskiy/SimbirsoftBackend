using Core.Entities;
using Core.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class BookSpecificationAuthorTitleGenre : BaseSpecification<Book>
    {
        public BookSpecificationAuthorTitleGenre(BookSpecParams specParams)
        {
            if (specParams.SortByAuthor)
                AddOrderBy(x => x.Author);
            if (specParams.SortByTitle)
                AddOrderBy(x => x.Author);
            if (specParams.SortByGenre)
                AddOrderBy(x => x.Author);
            if (specParams.Author != null)
            {
                if (specParams.Author.FirstName != null)
                    AddWhere(x => x.Author.FirstName.ToLower().Contains(specParams.Author.FirstName.ToLower()));
                if (specParams.Author.LastName != null)
                    AddWhere(x => x.Author.LastName.ToLower().Contains(specParams.Author.LastName.ToLower()));
                if (specParams.Author.MiddleName != null)
                    AddWhere(x => x.Author.MiddleName.ToLower().Contains(specParams.Author.MiddleName.ToLower()));
            }
            if (specParams.Genre != null)
                AddWhere(x => x.Genres.FirstOrDefault(f => f.GenreName == specParams.Genre.GenreName) != null);
            AddInclude(i => i.Author);
            AddInclude(i => i.Genres);
        }
    }
}

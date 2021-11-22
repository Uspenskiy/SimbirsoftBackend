using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookGenreService
    {
        Task<Boolean> UpdateBookGenres(Book book, IEnumerable<Genre> genres);
    }
}

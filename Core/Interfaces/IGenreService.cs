using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenres(IEnumerable<Genre> genres);
        Task<Genre> GetGenre(string name);
        Task<IEnumerable<Genre>> UpdateGenres(List<Genre> genres, IEnumerable<string> updateGenres);
    }
}

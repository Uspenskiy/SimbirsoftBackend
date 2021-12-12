using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BookService : IBookService
    {
        private readonly IGenreService _genreService;

        public BookService(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<Book> ResolveBookGenres(Book book)
        {
            book.Genres = (await _genreService.GetGenres(book.Genres)).ToList();
            return book;
        }

        public async Task<IEnumerable<Book>> ResolveBookGenres(IEnumerable<Book> books)
        {
            var result = new List<Book>();
            foreach (var book in books)
            {
                result.Add(await ResolveBookGenres(book));
            }
            return result;
        }
    }
}

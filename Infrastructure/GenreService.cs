﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GenreService : IGenreService
    {
        private readonly IGenericRepository<Genre> _genreRepository;

        public GenreService(IGenericRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetGenres(IEnumerable<string> names)
        {
            var result = new List<Genre>();
            foreach (var name in names)
                result.Add(await GetGenre(name));
            return result;
        }


        public async Task<Genre> GetGenre(string name)
        {
            var genreSpec = new GenreSpecification(name);
            var genre = await _genreRepository.GetEntityWithSpec(genreSpec);
            if (genre != null)
                return genre;
            _genreRepository.Add(new Genre { GenreName = name });
            await _genreRepository.SaveAsync();
            return await GetGenre(name);
        }

        public async Task<IEnumerable<Genre>> UpdateGenres(List<Genre> genres, IEnumerable<string> updateGenres)
        {
            var deleteName = genres.Select(s => s.GenreName)
                .Except(updateGenres)
                .FirstOrDefault();
            if (deleteName != null)
            {
                var genre = genres.FirstOrDefault(f => f.GenreName == deleteName);
                genres.Remove(genre);
                return genres;
            }
            var addName = updateGenres
                .Except(genres.Select(s => s.GenreName))
                .FirstOrDefault();
            genres.Add(await GetGenre(addName));
            return genres;
        }
    }
}
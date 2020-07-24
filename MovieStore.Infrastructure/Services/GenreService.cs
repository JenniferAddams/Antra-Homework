using Microsoft.Extensions.Caching.Memory;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private static readonly string _genresKey = "genres";
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromDays(30);
        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache)
        {
            //create instance of Genre Repo
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            // return await _genreRepository.ListAllAsync();
            // first check if the cache has genres by key
            var genres = await _memoryCache.GetOrCreateAsync(_genresKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _genreRepository.ListAllAsync();
            });
            return genres;
        }

        public async Task<IEnumerable<Genre>> GetGenresByMovieId(int movieId)
        {
            return await _genreRepository.GetGenresByMovieId(movieId);
        }
    }
}

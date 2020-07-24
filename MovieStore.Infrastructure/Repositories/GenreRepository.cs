using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieStoreDbContext dbContext): base(dbContext)
        {
        }

        public async Task<IEnumerable<Genre>> GetGenresByMovieId(int movieId)
        {
            var genres = await _dbContext.MovieGenres.Where(m => m.MovieId == movieId).Include(mg => mg.Genre)
                            .Select(g => g.Genre)
                            .ToListAsync();
            return genres;
        }
    }
}

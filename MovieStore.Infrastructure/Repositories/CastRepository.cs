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
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieStoreDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Cast>> GetAllCastsByMovieId(int movieId)
        {
            var casts = await _dbContext.MovieCasts.Where(m => m.MovieId == movieId).Include(mc => mc.Cast)
                .Select(c => c.Cast)
                .ToListAsync();
            return casts;
        }


    }
}

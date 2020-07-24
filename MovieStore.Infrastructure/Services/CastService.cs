using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;   
        }
        public async Task<IEnumerable<Cast>> GetAllCastsByMovieId(int movieId)
        {
            return await _castRepository.GetAllCastsByMovieId(movieId);
        }


    }
}

using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface ICastRepository: IAsyncRepository<Cast>
    {
        Task<IEnumerable<Cast>> GetAllCastsByMovieId(int movieId);

    }
}

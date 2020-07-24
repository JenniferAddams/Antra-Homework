using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface ICastService
    {
        Task<IEnumerable<Cast>> GetAllCastsByMovieId(int movieId);
 
    }
}

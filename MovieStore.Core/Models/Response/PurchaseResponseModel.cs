using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Models.Response
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public Task<IEnumerable<Movie>> PurchasedMovies { get; set; }

    }
}

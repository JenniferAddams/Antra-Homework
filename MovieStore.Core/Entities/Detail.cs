using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Detail
    {

        public Movie DetailMovie { get; set; }
        public IEnumerable<Cast> DetailCast { get; set; }
        public Decimal DetailRating { get; set; }
        public IEnumerable<Genre> DetailGenre { get; set; }
       // public IEnumerable<string> DetailCharacters { get; set; }

        public int DetailCurrentUserId { get; set; }
        public IEnumerable<Movie> purchasedMovie { get; set; }
        public bool isPurchased { get; set; }
        public bool IsFavorited { get; set; }
        public bool IsReviewed { get; set; }
        public class CastResponseModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string TmdbUrl { get; set; }
            public string ProfilePath { get; set; }
            public string Character { get; set; }
        }
    }
}

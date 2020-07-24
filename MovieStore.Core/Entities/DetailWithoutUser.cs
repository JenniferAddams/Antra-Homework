using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class DetailWithoutUser
    {
        public Movie DetailMovie { get; set; }
        public IEnumerable<Cast> DetailCast { get; set; }
        public Decimal DetailRating { get; set; }
        public IEnumerable<Genre> DetailGenre { get; set; }
        //public IEnumerable<MovieCast> DetailCharacters { get; set; }
        bool WithoutUser = true;
    }
}

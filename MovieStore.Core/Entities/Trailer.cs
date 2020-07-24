using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{
    //one Trailer belongs to single movie but one movie can have multiple trailers
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        // If some one gave you Trailer Id and you wanna find that Movie details, then this property will be useful(when people want other info from movie)
        // Navigation property
        public Movie Movie { get; set; }
    }
}

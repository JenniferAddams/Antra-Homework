using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace MovieStore.Core.Entities
{
    // genre class represents our domain model we are gonna have all the properities of genre table columns

    //data annotation change tablename Genre to
    [Table("Genre")]
    public class Genre
    {
        // By Convention EF is gonna consider any property in the entity class as Primary key for Id property
        public int Id { get; set; }
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}

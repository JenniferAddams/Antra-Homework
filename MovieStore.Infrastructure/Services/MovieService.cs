using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieStore.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICastRepository _castRepository;
        // Constructor Injection, inject MovieRepository class instance
        public MovieService(IMovieRepository movieRepository, ICastRepository castRepository)
        {
            _movieRepository = movieRepository;
            _castRepository = castRepository;
        }
        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            //getHighestRevenueMovies actuall get top 25
            return  await _movieRepository.GetHighestRevenueMovies();
        }

        public async Task<IEnumerable<Movie>> GetTop25RatedMovies()
        {
            //GetHighestRatedMovies actuall get top 25
            return await _movieRepository.GetHighestRatedMovies();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            //use instance of movieRepo to get movie   
            return await _movieRepository.GetByIdAsync(id);
        } 

        public async Task<Movie> CreateMovie(Movie movie)
        {
            //use instance of movieRepo to creare movie
            return await _movieRepository.AddAsync(movie);
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            //use instance of movieRepo to update movie
            return await _movieRepository.UpdateAsync(movie);
        }

        public async Task<int> GetMovieCount(string title = "")
        {
            if (string.IsNullOrEmpty(title))//check if title is null or not 
            {
                return await _movieRepository.GetCountAsync();//if  title is null not passing the title 
            }
            //title not null using instance of movierepo to call getcountAsync, since we already implement it in the EfRepo class
            return await _movieRepository.GetCountAsync(m => m.Title.Contains(title));
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            return await _movieRepository.GetMoviesByGenre(genreId);
        }

        public async Task<decimal> GetMoviesAverageRating(int Id)
        {
            return await _movieRepository.GetMoviesAverageRating(Id);
        }

        public async Task<IEnumerable<Cast>> GetAllCastsByMovieId(int id)
        {
           return await _castRepository.GetAllCastsByMovieId(id);
        }
    }
   /* public class MovieServiceTest : IMovieService
    {
        public async Task<IEnumerable<Movie>> GetTop25HighestRevenueMovies()
        {
            var movies = new List<Movie>()
                        {
                            new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                            new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                            new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                            new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                        };
            return movies;
        }
    }
   */

}

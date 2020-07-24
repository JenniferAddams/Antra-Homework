using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MovieStore.MVC.Filters;
using Microsoft.AspNetCore.Identity;
using MovieStore.Core.RepositoryInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class MoviesController : Controller
    {
        // IOC, ASP.NET Core has built-in IOC/DI
        // In .NET Framework we need to to rely on third-party IOC to do Dependency Injection, Autofac, Ninject
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly IGenreService _genreService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MoviesController(IMovieService movieService,ICastService castService,IGenreService genreService, IHttpContextAccessor httpContextAccessor,IUserService userService)
        {
            _movieService = movieService;
            _castService = castService;
            _genreService = genreService;
            _httpContextAccessor = httpContextAccessor;
            _userService= userService;
           
        }
        //  GET localhost/Movies/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // call our Movie Service ,method, highest grossing method
            var movies = await _movieService.GetTop25HighestRevenueMovies();
            return View(movies);
        }

        
        
        [HttpGet]
        public async Task<IActionResult> Genres(int genreId)
        { 
            var movies = await _movieService.GetMoviesByGenre(genreId);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            //var email = User.Identity.Name;
            //var currentUser = await _userService.GetUserByEmail(email);
            //var currentId = currentUser.Id;

            var movie = await _movieService.GetMovieById(Id);
            var cast = await _castService.GetAllCastsByMovieId(Id);
            var rat = await _movieService.GetMoviesAverageRating(Id);
            var genre = await _genreService.GetGenresByMovieId(Id);


            var currentIdstr = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var purchaseOrNot = false;
            var favOrNot = false;
            var reviewed = false;
            var currentId = 0;

            if(currentIdstr!=null && !string.IsNullOrWhiteSpace(currentIdstr.Value))
            {
                currentId = Int32.Parse(currentIdstr.Value);
                favOrNot = await _userService.IsMovieFavorited(currentId, Id);
                reviewed = await _userService.IsMovieReviewed(currentId, Id);
            }

            Detail detail = new Detail()
            {
                DetailMovie = movie,
                DetailCast = cast,
                DetailRating = rat,
                DetailGenre = genre,
                //DetailCharacters = chars,

                DetailCurrentUserId = currentId,
                isPurchased = purchaseOrNot,
                IsFavorited = favOrNot,
                IsReviewed=reviewed
                };
                return View(detail);   
        }       
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    //any c# can become a mvc controller if it inherits from controller base class microsoft.Aspnetcore.mvc
    // GET http://localhost:2322/home/index
    //routing -- pattern matching technique
    //homecontroller
    //index -- action method 
    public class HomeController : Controller
    {
        private readonly IMovieService _MovieService;
        public HomeController(IMovieService movieService)
        {
            _MovieService = movieService;
        }
        // we need or Movie Card we are going to use that one in lots of places...
        // 1. Home page -- show top revenue movies --> Movie Card
        // 2. Genres/show Movies belonging to that genre --> Movie Card
        // 3. Top Rated Movies --> Top Movies as a card

        // We have to create this Movie Card in such a way that it can be reused in lots of places
        // Partial Views will help us in creating reusable Views across the application
        // Partial views are views inside another view

        public async Task<IActionResult> Index()
        {
            //by default MVC will look for same View Name as ation method name
            //it will look inside view folder -->Home(same name as controller)-->index.cshtml(c#+html razor views)

            //1 program.cs -->main method
            //2 startup class
            //3 configureservices
            //4 configure
            //5 homeController
            //6 action method 
            //7 return view

            //middleware a piece of software logic taht will be executed
            // train DC to boston , multiple stops(middleware) 
            //request --> M1 some logic -->m2->next m3 --> response and then goback wards 
            //return View();// view return viewResult and viewresult implement IACtion interface

            var movies = await _MovieService.GetTop25HighestRevenueMovies();
            return View(movies);

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

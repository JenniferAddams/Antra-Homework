using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Purchase()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult Review()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Review(ReviewRequestModel reviewRequestModel)
        {
            await _userService.AddMovieReview(reviewRequestModel);
            return View(reviewRequestModel);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Reviews()
        {
            var email = User.Identity.Name;
            var currentUser = await _userService.GetUserByEmail(email);
            var currentId = currentUser.Id;
            var allReviews = await _userService.GetAllReviewsByUser(currentId);

            return View(allReviews);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Favorite(FavoriteRequestModel favoriteRequest)
        {
            string url = HttpContext.Request.GetDisplayUrl();
            await _userService.AddFavorite(favoriteRequest);
            return View();
        }
        [HttpGet]
        [Route("User/{id}/movie/{movieId}")]
        public async Task<ActionResult> Favorite(int id,int movieId)
        {
            bool isMyFav = await _userService.IsMovieFavorited(id, movieId);
            return View(isMyFav);

        }
       [Authorize]
       [HttpPost]
       public async Task<ActionResult> DeleteFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            await _userService.RemoveFavorite(favoriteRequestModel);
            return View();
        }

        //filter in asp.net [atttibute]
        //some piece of code that tuns before an controller or action  method executes or when some event happens
        //that run befor or after specific stages in http pipeline
        //1 authorization
        //2 action filter
        //3 result filter
        //4. exception filter, but in real world we use exception middleware to catch exception 
        //5. resource filter 
  
    }
}

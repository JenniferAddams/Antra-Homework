using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IUserService
    {
        
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> ValidateUser(string email, string password);    
        Task<User> GetUserByEmail(string email);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
        Task<bool> IsMovieReviewed(int id, int movieId);
        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> IsMovieFavorited(int id, int movieId);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);       
    }
}

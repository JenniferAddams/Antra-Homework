using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService, IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;

            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }
        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {

            var dbFavorite = await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId &&
                                                         r.MovieId == favoriteRequest.MovieId);
  
            await _favoriteRepository.DeleteAsync(dbFavorite.First());

        }
        public async Task<bool> IsMovieFavorited(int userId, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == userId);
        }
        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var newFavorite = new Favorite
            {
                UserId=favoriteRequest.UserId,
                MovieId=favoriteRequest.MovieId
            };
            await _favoriteRepository.AddAsync(newFavorite);
        }
        public async Task<bool> IsMovieReviewed(int id, int movieId)
        {
            return await _reviewRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
        }
        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId=reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText=reviewRequest.ReviewText
            };
            var createdReview = await _reviewRepository.AddAsync(review);
        }
      public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
        {
            return await _reviewRepository.GetAllReviewsMakeByUser (id);
        }





        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }



        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Step 1 : Check whether this user already exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
            {
                // we already have this user(email) in our table
                // return or throw an exception saying Conflict user
                throw new Exception("User already registered, Please try to Login");
            }
            // Step 2 : lets Generate a random unique Salt
            var salt = _cryptoService.GenerateSalt();
            // Never ever craete your own Hashing Algorithm, always use Industry tested/tried Hashing Algorithm
            // Step 3: we  hash the password with the salt created in the above step
            var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);
            // craete User object so that we can save it to User Table
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            // Step 4: Save it to Database
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // Step 1 : Get user record from the databse by email;
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                // user does not even exists
                throw new Exception("Register first, user does not exists");
            }
            // Step 2: we need to hash the password that user entered in the npage with Salt from the database from step1
            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);
            // Step 3 : Compare the databse hashed password with Hashed passowrd genereated in step 2
            if (hashedPassword == user.HashedPassword)
            {
                // user entred right password
                // send some user details
                var response = new UserLoginResponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email
                };
                return response;
            }
            return null;
        }


    }
}

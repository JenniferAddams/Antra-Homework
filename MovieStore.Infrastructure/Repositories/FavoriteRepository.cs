using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Infrastructure.Repositories
{
    public class FavoriteRepository:EfRepository<Favorite>,IFavoriteRepository
    {
        public FavoriteRepository(MovieStoreDbContext dbContext) : base(dbContext)
        {
        }


    }
}

using w10d4.Repositories;
using w10d4.Models;
using System.Collections.Generic;

namespace w10d4.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _repo;

        public FavoritesService(FavoritesRepository repo)
        {
            _repo = repo;
        }

        internal List<RecipeFavoriteVM> GetByAccountId(string id)
        {
            return _repo.GetByAccountId(id);
        }

        internal List<AccountFavoriteVM> GetByRecipeId(int id)
        {
            return _repo.GetByRecipeId(id);
        }
    }
}
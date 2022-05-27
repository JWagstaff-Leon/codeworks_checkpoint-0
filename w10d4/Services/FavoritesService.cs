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

        internal Favorite GetById(int id)
        {
            Favorite found = _repo.GetById(id);
            if(found == null)
            {
                throw new System.Exception("Could not find favorite with that id.");
            }
            return found;
        }

        internal Favorite Create(Favorite data)
        {
            Favorite created = _repo.Create(data);
            return created;
        }

        internal Favorite Remove(int id, string userId)
        {
            Favorite removed = GetById(id);
            if(removed.AccountId != userId)
            {
                throw new System.Exception("You do not have permission to delete this favorite.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}
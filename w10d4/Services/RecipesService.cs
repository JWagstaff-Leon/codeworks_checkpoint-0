using System.Collections.Generic;
using w10d4.Models;
using w10d4.Repositories;

namespace w10d4.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal List<Recipe> GetAll()
        {
            return _repo.GetAll();
        }

        internal Recipe GetById(int id)
        {
            Recipe found = _repo.GetById(id);
            if(found == null)
            {
                throw new System.Exception("Could not find recipe with that id.");
            }
            return found;
        }

        internal List<Recipe> GetByCategory(string category)
        {
            return _repo.GetByCategory(category);
        }

        internal List<Recipe> GetByCreatorId(string creatorId)
        {
            return _repo.GetByCreatorId(creatorId);
        }

        internal Recipe Create(Recipe data)
        {
            return _repo.Create(data);
        }

        internal Recipe Edit(Recipe update)
        {
            Recipe edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new System.Exception("You do not have permission to edit this recipe.");
            }
            return _repo.Edit(update);
        }

        internal Recipe Remove(int id, string userId)
        {
            Recipe removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new System.Exception("You do not have permission to delete this recipe.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}
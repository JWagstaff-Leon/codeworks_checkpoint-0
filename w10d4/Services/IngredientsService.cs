using System.Collections.Generic;
using w10d4.Models;
using w10d4.Repositories;

namespace w10d4.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _repo;

        public IngredientsService(IngredientsRepository repo)
        {
            _repo = repo;
        }

        internal Ingredient GetById(int id)
        {
            Ingredient found = _repo.GetById(id);
            if(found == null)
            {
                throw new System.Exception("Could not find ingredient with that id.");
            }
            return found;
        }

        internal List<Ingredient> GetByRecipeId(int recipeId)
        {
            return _repo.GetByRecipeId(recipeId);
        }

        internal Ingredient Create(Ingredient data)
        {
            return _repo.Create(data);
        }

        internal Ingredient Edit(Ingredient update)
        {
            Ingredient edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new System.Exception("You do not have permission to edit this ingredient");
            }
            edited.Quantity = update.Quantity ?? edited.Quantity;
            edited.Name = update.Name ?? edited.Name;
            return _repo.Edit(edited);
        }

        internal Ingredient Remove(int id, string creatorId)
        {
            Ingredient removed = GetById(id);
            if(removed.CreatorId != creatorId)
            {
                throw new System.Exception("You do not have permission to delete this ingredient.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}
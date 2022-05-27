using w10d4.Repositories;
using System.Collections.Generic;
using w10d4.Models;

namespace w10d4.Services
{
    public class StepsService
    {
        private readonly StepsRepository _repo;

        public StepsService(StepsRepository repo)
        {
            _repo = repo;
        }

        internal Step GetById(int id)
        {
            Step found = _repo.GetById(id);
            if(found == null)
            {
                throw new System.Exception("Could not find step with that id.");
            }
            return found;
        }

        internal List<Step> GetByRecipeId(int id)
        {
            return _repo.GetByRecipeId(id);
        }

        internal Step Create(Step data)
        {
            return _repo.Create(data);
        }

        internal Step Edit(Step update)
        {
            Step edited = GetById(update.Id);
            if(edited.CreatorId != update.CreatorId)
            {
                throw new System.Exception("You do not have permission to edit this step.");
            }
            edited.Body = update.Body ?? edited.Body;
            edited.Position = update.Position;
            return _repo.Edit(edited);
        }
        
        internal Step Remove(int id, string userId)
        {
            Step removed = GetById(id);
            if(removed.CreatorId != userId)
            {
                throw new System.Exception("You do not have permission to delete this step.");
            }
            _repo.Remove(id);
            return removed;
        }
    }
}
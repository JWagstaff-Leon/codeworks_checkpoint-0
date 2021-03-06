using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w10d4.Models;
using w10d4.Services;

namespace w10d4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _serv;
        private readonly IngredientsService _ingredientsServ;
        private readonly StepsService _stepsServ;
        private readonly FavoritesService _favoritesServ;

        public RecipesController(RecipesService serv, IngredientsService ingredientsServ, StepsService stepsServ, FavoritesService favoritesServ)
        {
            _serv = serv;
            _ingredientsServ = ingredientsServ;
            _stepsServ = stepsServ;
            _favoritesServ = favoritesServ;
        }

        [HttpGet]
        public ActionResult<List<Recipe>> GetAll()
        {
            try
            {
                List<Recipe> found = _serv.GetAll();
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetById(int id)
        {
            try
            {
                Recipe found = _serv.GetById(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/ingredients/")]
        public ActionResult<List<Ingredient>> GetIngredients(int id)
        {
           try
           {
               List<Ingredient> found = _ingredientsServ.GetByRecipeId(id);
               return Ok(found);
           }
           catch(Exception e)
           {
               return BadRequest(e.Message);
           }
        }

        [HttpGet("{id}/steps")]
        public ActionResult<Step> GetSteps(int id)
        {
            try
            {
                List<Step> found = _stepsServ.GetByRecipeId(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/favorites")]
        public ActionResult<List<RecipeFavoriteVM>> GetFavoriteRecipes(int id)
        {
            try
            {
                List<AccountFavoriteVM> found = _favoritesServ.GetByRecipeId(id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> Create([FromBody] Recipe data)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = userInfo.Id;
                Recipe created = _serv.Create(data);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/favorites")]
        [Authorize]
        public async Task<ActionResult<Favorite>> Create(int id)
        {
            try
            {
                Favorite data = new Favorite();
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                data.AccountId = userInfo.Id;
                data.RecipeId = id;
                Favorite created = _favoritesServ.Create(data);
                return Ok(created);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Edit([FromBody] Recipe update, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                update.CreatorId = userInfo.Id;
                update.Id = id;
                Recipe edited = _serv.Edit(update);
                return Ok(edited);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Remove(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using w10d4.Models;
using w10d4.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace w10d4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly FavoritesService _favoritesService;

        public AccountController(AccountService accountService, FavoritesService favoritesService)
        {
            _accountService = accountService;
            _favoritesService = favoritesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("favorites")]
        [Authorize]
        public async Task<ActionResult<List<RecipeFavoriteVM>>> GetFavoriteRecipes()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<RecipeFavoriteVM> found = _favoritesService.GetByAccountId(userInfo.Id);
                return Ok(found);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}
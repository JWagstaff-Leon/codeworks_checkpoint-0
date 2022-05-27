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
    public class FavoritesController : ControllerBase
    {
        private readonly FavoritesService _serv;

        public FavoritesController(FavoritesService serv)
        {
            _serv = serv;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Favorite>> Create(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Favorite removed = _serv.Remove(id, userInfo.Id);
                return Ok(removed);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
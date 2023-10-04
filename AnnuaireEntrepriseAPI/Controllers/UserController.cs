using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntrepriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AnnuaireEntrepriseContext _context;

        public UserController(AnnuaireEntrepriseContext context) 
        {
            _context = context;
        }

        #region Méthodes GET
        [HttpGet("[action]")]
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(item => item.SiteNavigation).Include(item => item.ServiceNavigation).ToArray();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userResult = _context.Users.Include(item => item.SiteNavigation).Include(item => item.ServiceNavigation).Where(item => item.Id == id).ToArray();
            return Ok(userResult);
        }
        #endregion

       
    }
}

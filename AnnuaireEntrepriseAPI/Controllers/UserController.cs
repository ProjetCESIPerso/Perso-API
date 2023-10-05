using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using System.Net;

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
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "La récupération de tous les utilisateurs a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table user est vide")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            if (!_context.Users.Any())
            {
                return NoContent();

            }

            return _context.Users.ToArray();
        }

        [HttpGet("[action]/{id}", Name = "GetUserById")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "La récupération de l'utilisateur a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table user est vide")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID de l'utilisateur renseigné n'est pas connu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!_context.Users.Any())
            {
                return NoContent();

            }

            //Vérification si le produit avec l'id renseigné existe
            //var siteToFind = _context.Sites.Find(name); //Plante car name doit être un int
            //if (siteToFind == null)
            //{
            //    return NotFound();
            //}

            var userResult = _context.Users.Where(item => item.Id == id).Single();

            //if (siteResult == null) { return NotFound(); }

            return Ok(userResult);
        }
        #endregion

        #region Méthodes POST

        [HttpPost("[action]")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(User), Description = "La création de l'utilisateur a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(User), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //Verif si le site et le service existe bien
            Site verifSite = _context.Sites.Where(item => item.Town == user.SiteNavigation.Town).Single();
            if (verifSite == null)
            {
                return BadRequest();
            }

            Service verifService = _context.Services.Where(item => item.Name == user.ServiceNavigation.Name).Single();
            if(verifService == null)
            {
                return BadRequest();
            }

            User finalUser = new User();

            finalUser.Name = user.Name;
            finalUser.Surname = user.Surname;
            finalUser.Email = user.Email;
            finalUser.PhoneNumber = user.PhoneNumber;
            finalUser.MobilePhone = user.MobilePhone;
            finalUser.Service = user.ServiceNavigation.Name;
            finalUser.Site = user.SiteNavigation.Town;


            _context.Users.AddAsync(finalUser);
            _context.SaveChanges();
            var result = _context.Users.Where(item => item.Id == user.Id).ToArray();
            if (result is not null)
                return CreatedAtRoute("GetUserById", new { name = finalUser.Id }, finalUser);
            else
                return Ok(Enumerable.Empty<Site>());

        }

        [HttpPost("[action]/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(EmptyResult), Description = "La suppression de l'utilisateur a été effectué avec succès")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID de l'utilisateur est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            User userBdd = _context.Users.Where(item => item.Id == id).Single();

            if (userBdd == null)
                return NotFound();

            try
            {
                _context.Users.Remove(userBdd);
                _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }
        #endregion

        #region Méthode PUT
        [HttpPut("[action]/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "La mise à jour de l'utilisateur a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(User), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID de l'utilisateur renseigné est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSite(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userBdd = _context.Users.Where(item => item.Id == id).FirstOrDefault();

            if (userBdd == null)
            {
                return NotFound("L'ID du site envoyé n'est pas correct");
            }

            //Verif si le site et le service existe bien
            if (_context.Sites.Where(item => item.Town == user.SiteNavigation.Town).Any())
            {
                return BadRequest();
            }

            if (_context.Services.Where(item => item.Name == user.ServiceNavigation.Name).Any())
            {
                return BadRequest();
            }

            userBdd.Name = user.Name;
            userBdd.Surname = user.Surname;
            userBdd.Email = user.Email;
            userBdd.PhoneNumber = user.PhoneNumber;
            userBdd.MobilePhone = user.MobilePhone;
            userBdd.Service = user.Service;
            userBdd.Site = user.Site;

            _context.Entry(userBdd).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return Ok(user);
        }
        #endregion


    }
}

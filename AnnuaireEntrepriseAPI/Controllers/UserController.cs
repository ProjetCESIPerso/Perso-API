using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.DTOs;
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
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            if (!_context.Users.Any())
            {
                return NoContent();
            }

            var usersAllBDD = _context.Users.Include(item => item.Site).Include(item => item.Service).ToList();

            var usersAll = new List<UserDTO>();

            foreach (var user in usersAllBDD)
            {
                usersAll.Add(new UserDTO 
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    MobilePhone = user.MobilePhone,
                    PhoneNumber = user.PhoneNumber,
                    ServiceId = user.Service.Id,
                    SiteId = user.Site.Id
                });
            }

            return usersAll;
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
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
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

            var userResultBDD = _context.Users.Where(item => item.Id == id).Include(item => item.Service).Include(item => item.Site).Single();

            var userResult = new UserDTO();

            userResult.Id = userResultBDD.Id;
            userResult.Name = userResultBDD.Name;
            userResult.Surname = userResultBDD.Surname;
            userResult.Email = userResultBDD.Email;
            userResult.PhoneNumber = userResultBDD.PhoneNumber;
            userResult.MobilePhone = userResultBDD.MobilePhone;
            userResult.ServiceId = userResultBDD.Service.Id;
            userResult.SiteId = userResultBDD.Site.Id;

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
        public async Task<IActionResult> AddUser(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //vérif doublon
            if (_context.Users.Where(item => item.Name == user.Name && item.Surname == user.Surname && item.Email == user.Email && item.MobilePhone == user.MobilePhone && item.PhoneNumber == user.PhoneNumber).Count() > 0)
            {
                return Conflict(); //Manque phrase pour dire que déja existant
            }

            var test = _context.Sites.Where(item => item.Id == user.SiteId).Any();
            //Verif si le site et le service existe bien
            if (_context.Sites.Where(item => item.Id == user.SiteId).Any() == false)
            {
                return BadRequest();
            }

            if(_context.Services.Where(item => item.Id == user.ServiceId).Any() == false)
            {
                return BadRequest();
            }

            User finalUser = new User();

            finalUser.Name = user.Name;
            finalUser.Surname = user.Surname;
            finalUser.Email = user.Email;
            finalUser.PhoneNumber = user.PhoneNumber;
            finalUser.MobilePhone = user.MobilePhone;
            finalUser.Service = _context.Services.Where(item => item.Id == user.ServiceId).Single();
            finalUser.Site = _context.Sites.Where(item => item.Id == user.SiteId).Single();

            _context.Users.AddAsync(finalUser);
            _context.SaveChanges();
            var result = _context.Users.Where(item => item.Name == finalUser.Name && item.Surname == finalUser.Surname && item.Email == finalUser.Email).Single();
            if (result is not null)
                return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
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
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User userBdd = _context.Users.Where(item => item.Id == id).Include(item => item.Service).Include(item => item.Site).FirstOrDefault();

            if (userBdd == null)
            {
                return NotFound("L'ID du site envoyé n'est pas correct");
            }

            //Verif si le site et le service existe bien
            if (_context.Sites.Where(item => item.Id == user.SiteId).Any() == false)
            {
                return BadRequest();
            }

            if (_context.Services.Where(item => item.Id == user.ServiceId).Any() == false)
            {
                return BadRequest();
            }

            userBdd.Name = user.Name;
            userBdd.Surname = user.Surname;
            userBdd.Email = user.Email;
            userBdd.PhoneNumber = user.PhoneNumber;
            userBdd.MobilePhone = user.MobilePhone;
            userBdd.Service = _context.Services.Where(item => item.Id == user.ServiceId).Single();
            userBdd.Site = _context.Sites.Where(item => item.Id == user.SiteId).Single();

            _context.Entry(userBdd).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return Ok(user);
        }
        #endregion


    }
}

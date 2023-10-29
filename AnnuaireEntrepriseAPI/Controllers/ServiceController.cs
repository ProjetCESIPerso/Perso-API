using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using NSwag.Annotations;
using AnnuaireEntrepriseAPI.DTOs;
using System.Linq;
using AnnuaireEntrepriseAPI.Interfaces;

namespace AnnuaireEntrepriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase, IServiceController
    {
        private readonly AnnuaireEntrepriseContext _context;
        public ServiceController(AnnuaireEntrepriseContext context) 
        {
            _context = context;
        }

        #region Méthodes GET

        [HttpGet("[action]")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Service), Description = "La récupération de tous les services a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table service est vide")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ServiceDTO>> GetAll()
        {
            if (!_context.Services.Any())
            {
                return NoContent();

            }

            var serviceAllBDD = _context.Services.ToList();

            var serviceAll = new List<ServiceDTO>();

            foreach (var service in serviceAllBDD)
            {
                serviceAll.Add(new ServiceDTO
                {
                    Id = service.Id,
                    Name = service.Name,
                });
            }

            return serviceAll;
        }

        [HttpGet("[action]/{id}", Name = "GetServiceById")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Service), Description = "La récupération du service a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table service est vide")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du service renseigné n'est pas connu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDTO>> GetServiceById(int id)
        {
            if (!_context.Services.Any())
            {
                return NoContent();

            }

            //Vérification si le produit avec l'id renseigné existe
            //var serviceToFind = _context.Services.Find(name); //Plante car name doit être un int
            //if (serviceToFind == null)
            //{
            //    return NotFound();
            //}

            var serviceResultBDD = _context.Services.Where(item => item.Id == id).Single();

            var serviceResult = new ServiceDTO();

            serviceResult.Id = serviceResultBDD.Id;
            serviceResult.Name = serviceResultBDD.Name;

            //if (serviceResult == null) { return NotFound(); }

            return Ok(serviceResult);
        }

        [HttpGet("[action]/{name}", Name = "GetServiceByName")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Service), Description = "La récupération du service a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table service est vide")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "Le nom du service renseigné n'est pas connu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDTO>> GetServiceByName(string name)
        {
            if (!_context.Services.Any())
            {
                return NoContent();

            }

            //Vérification si le produit avec l'id renseigné existe
            //var serviceToFind = _context.Services.Find(name); //Plante car name doit être un int
            //if (serviceToFind == null)
            //{
            //    return NotFound();
            //}
            if (!_context.Services.Where(item => item.Name == name).Any()) return NoContent();

            var serviceResultBDD = _context.Services.Where(item => item.Name == name).Single();

            var serviceResult = new ServiceDTO();

            serviceResult.Id = serviceResultBDD.Id;
            serviceResult.Name = serviceResultBDD.Name;

            

            return Ok(serviceResult);
        }
        #endregion

        #region Méthodes POST

        [HttpPost("[action]/{name}")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Service), Description = "La création du service a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Service), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddService(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //vérif doublon
            if (_context.Services.Where(item => item.Name.ToUpper() == name.ToUpper()).Count() > 0)
            {
                return Conflict(); //Manque phrase pour dire que déja existant
            }

            Service serviceBdd = new Service();
            serviceBdd.Name = name;

            _context.Services.AddAsync(serviceBdd);
            _context.SaveChanges();
            var result = _context.Services.Where(item => item.Name == name).ToArray();
            if (result is not null)
            {
                var serviceToServiceDTO = new ServiceDTO();
                serviceToServiceDTO.Id = serviceBdd.Id;
                serviceToServiceDTO.Name = serviceBdd.Name;
                
                return CreatedAtRoute("GetServiceById", new { id = serviceToServiceDTO.Id }, serviceToServiceDTO);
            }
            else
                return Ok(Enumerable.Empty<Service>());

        }

        [HttpPost("[action]/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(EmptyResult), Description = "La suppression du service a été effectué avec succès")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du service est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteService(string name)
        {
            Service serviceBdd = _context.Services.Where(item => item.Name == name).Single();

            if (serviceBdd == null)
                return NotFound();

            try
            {
                _context.Services.Remove(serviceBdd);
                _context.SaveChangesAsync();

                return Ok(true);
            }
            catch(DbUpdateException)
            {
                return Conflict();
            }
        }
        #endregion

        #region Méthode PUT
        [HttpPut("[action]/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Service), Description = "La mise à jour du service a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Service), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du service renseigné est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateService(string name, ServiceDTO service)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Service serviceBdd = _context.Services.Where(item => item.Name == name).FirstOrDefault();

            if (serviceBdd == null)
            {
                return NotFound("L'ID du service envoyé n'est pas correct");
            }

            serviceBdd.Name = service.Name;

            _context.Entry(serviceBdd).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return Ok(service);
        }
        #endregion
    }
}

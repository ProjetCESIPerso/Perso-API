﻿using AnnuaireEntrepriseAPI.Database;
using AnnuaireEntrepriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using NSwag.Annotations;
using AnnuaireEntrepriseAPI.DTOs;

namespace AnnuaireEntrepriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly AnnuaireEntrepriseContext _context;
        public SiteController(AnnuaireEntrepriseContext context) 
        {
            _context = context;
        }

        #region Méthodes GET

        [HttpGet("[action]")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Site), Description = "La récupération de tous les sites a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table site est vide")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SiteDTO>> GetAll()
        {
            if (!_context.Sites.Any())
            {
                return NoContent();

            }

            var siteAllBDD = _context.Sites.ToList();

            var siteAll = new List<SiteDTO>();

            foreach (var site in siteAllBDD)
            {
                siteAll.Add(new SiteDTO
                {
                    Id = site.Id,
                    Town = site.Town,
                });
            }

            return siteAll;
        }

        [HttpGet("[action]/{id}", Name = "GetSiteById")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Site), Description = "La récupération du site a été un succès")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(EmptyResult), Description = "La table site est vide")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du site renseigné n'est pas connu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SiteDTO>> GetSiteById(int id)
        {
            if (!_context.Sites.Any())
            {
                return NoContent();

            }

            //Vérification si le produit avec l'id renseigné existe
            //var siteToFind = _context.Sites.Find(name); //Plante car name doit être un int
            //if (siteToFind == null)
            //{
            //    return NotFound();
            //}

            var siteResultBDD = _context.Sites.Where(item => item.Id == id).Single();

            var siteResult = new SiteDTO();

            siteResult.Id = siteResultBDD.Id;
            siteResult.Town = siteResultBDD.Town;

            //if (siteResult == null) { return NotFound(); }

            return Ok(siteResult);
        }
        #endregion

        #region Méthodes POST

        [HttpPost("[action]/{site}")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Site), Description = "La création du site a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Site), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSite(string site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            //vérif doublon
            if (_context.Sites.Where(item => item.Town.ToUpper() == site.ToUpper()).Count() > 0)
            {
                return Conflict(); //Manque phrase pour dire que déja existant
            }

            Site siteBdd = new Site();
            siteBdd.Town = site;

            _context.Sites.AddAsync(siteBdd);
            _context.SaveChanges();
            var result = _context.Sites.Where(item => item.Town == site).ToArray();
            if (result is not null)
            {
                var siteToSiteDTO = new SiteDTO();
                siteToSiteDTO.Id = siteBdd.Id;
                siteToSiteDTO.Town = siteBdd.Town;

                return CreatedAtRoute("GetSiteById", new { id = siteToSiteDTO.Town }, siteToSiteDTO);
            }
            else
                return Ok(Enumerable.Empty<Site>());

        }

        [HttpPost("[action]/{site}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(EmptyResult), Description = "La suppression du site a été effectué avec succès")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du site est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSite(string site)
        {
            Site siteBdd = _context.Sites.Where(item => item.Town == site).Single();

            if (siteBdd == null)
                return NotFound();

            try
            {
                _context.Sites.Remove(siteBdd);
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
        [HttpPut("[action]/{town}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Site), Description = "La mise à jour du site a été un succès")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Site), Description = "Le fichier Json est incorrect")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(EmptyResult), Description = "Une ou plusieurs valeurs dépassent le nombre de caractère autorisé")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(EmptyResult), Description = "L'ID du site renseigné est inconnu de la base de données")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(EmptyResult), Description = "Erreur serveur interne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSite(string town, Site site)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Site siteBdd = _context.Sites.Where(item => item.Town == town).FirstOrDefault();

            if (siteBdd == null)
            {
                return NotFound("L'ID du site envoyé n'est pas correct");
            }

            siteBdd.Town = site.Town;

            _context.Entry(siteBdd).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return Ok(site);
        }
        #endregion
    }
}

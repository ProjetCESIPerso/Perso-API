using AnnuaireEntrepriseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnnuaireEntrepriseAPI.Interfaces
{
    public interface ISiteController
    {
        /// <summary>
        /// Récupère tout les sites
        /// </summary>
        /// <returns>Retourne une liste de site</returns>
        ActionResult<IEnumerable<SiteDTO>> GetAll();

        /// <summary>
        /// Récupère un site par ID
        /// </summary>
        /// <param name="id">Id du site</param>
        /// <returns>Le site</returns>
        Task<ActionResult<SiteDTO>> GetSiteById(int id);

        /// <summary>
        /// Récupère un site par nom
        /// </summary>
        /// <param name="name">Nom du site</param>
        /// <returns>Le site</returns>
        Task<ActionResult<SiteDTO>> GetSiteByName(string name);

        /// <summary>
        /// Ajoute un site
        /// </summary>
        /// <param name="name">Nom du site à ajouter</param>
        /// <returns></returns>
        Task<IActionResult> AddSite(string name);

        /// <summary>
        /// Supprime un site
        /// </summary>
        /// <param name="name">Nom du site à supprimer</param>
        /// <returns></returns>
        Task<IActionResult> DeleteSite(string name);

        /// <summary>
        /// Modifier un site
        /// </summary>
        /// <param name="name">Nom du site à modifier</param>
        /// <param name="site">Données à modifier</param>
        /// <returns></returns>
        Task<IActionResult> UpdateSite(string name, SiteDTO site);
    }
}

using AnnuaireEntrepriseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnnuaireEntrepriseAPI.Interfaces
{
    public interface IServiceController
    {
        /// <summary>
        /// Récupère tout les services
        /// </summary>
        /// <returns>Retourne une liste de service</returns>
        ActionResult<IEnumerable<ServiceDTO>> GetAll();

        /// <summary>
        /// Récupère un service par ID
        /// </summary>
        /// <param name="id">Id du service</param>
        /// <returns>Le service</returns>
        Task<ActionResult<ServiceDTO>> GetServiceById(int id);

        /// <summary>
        /// Récupère un service par nom
        /// </summary>
        /// <param name="name">Nom du service</param>
        /// <returns>Le service</returns>
        Task<ActionResult<ServiceDTO>> GetServiceByName(string name);

        /// <summary>
        /// Ajoute un service
        /// </summary>
        /// <param name="name">Nom du service à ajouter</param>
        /// <returns></returns>
        Task<IActionResult> AddService(string name);

        /// <summary>
        /// Supprime un service
        /// </summary>
        /// <param name="name">Nom du service à supprimer</param>
        /// <returns></returns>
        Task<IActionResult> DeleteService(string name);

        /// <summary>
        /// Modifier un service
        /// </summary>
        /// <param name="name">Nom du service à modifier</param>
        /// <param name="service">Données à modifier</param>
        /// <returns></returns>
        Task<IActionResult> UpdateService(string name, ServiceDTO service);
    }
}

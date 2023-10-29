using AnnuaireEntrepriseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AnnuaireEntrepriseAPI.Interfaces
{
    public interface IUserController
    {
        /// <summary>
        /// Récupère tout les utilisateurs
        /// </summary>
        /// <returns>Retourne une liste d'utilisateurs</returns>
        ActionResult<IEnumerable<UserDTO>> GetAll();

        /// <summary>
        /// Récupère un utilisateur par ID
        /// </summary>
        /// <param name="id">Id de l'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        Task<ActionResult<UserDTO>> GetUserById(int id);

        /// <summary>
        /// Ajoute un utilisateur
        /// </summary>
        /// <param name="user">Données de l'utilisateur à ajouter</param>
        /// <returns></returns>
        Task<IActionResult> AddUser(UserDTO user);

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer</param>
        /// <returns></returns>
        Task<IActionResult> DeleteUser(int id);

        /// <summary>
        /// Modifier un utilisateur
        /// </summary>
        /// <param name="id">ID de l'utilisateur à modifier</param>
        /// <param name="site">Données à modifier</param>
        /// <returns></returns>
        Task<ActionResult<UserDTO>> UpdateUser(int id, UserDTO site);
    }
}

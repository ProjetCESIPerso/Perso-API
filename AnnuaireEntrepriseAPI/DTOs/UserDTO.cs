using AnnuaireEntrepriseAPI.Models;

namespace AnnuaireEntrepriseAPI.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string MobilePhone { get; set; }

        public int ServiceId { get; set; }

        public int SiteId { get; set; }
    }
}

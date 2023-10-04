namespace AnnuaireEntrepriseAPI.Models;

public partial class Site
{
    public int Id { get; set; }
    public string Town { get; set; } = null!;

    //public virtual ICollection<User> Users { get; set; } = new List<User>();
}

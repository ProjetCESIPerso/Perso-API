namespace AnnuaireEntrepriseAPI.Models;

public partial class Site
{
    public int Id { get; set; }
    public string Town { get; set; } = null!;

    public ICollection<User> Users { get; set; }
}

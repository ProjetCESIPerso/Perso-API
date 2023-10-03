using System;
using System.Collections.Generic;

namespace AnnuaireEntrepriseAPI.Models;

public partial class Site
{
    public string Town { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

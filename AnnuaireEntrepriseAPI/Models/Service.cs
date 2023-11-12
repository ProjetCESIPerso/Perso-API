using System;
using System.Collections.Generic;

namespace AnnuaireEntrepriseAPI.Models;

public partial class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    //public ICollection<User> Users { get; set; }
}

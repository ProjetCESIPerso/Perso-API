using System;
using System.Collections.Generic;

namespace AnnuaireEntrepriseAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; } 

    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string MobilePhone { get; set; }

    public Service Service { get; set; }

    public Site Site { get; set; }
}

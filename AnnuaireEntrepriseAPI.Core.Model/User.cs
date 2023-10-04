using System;
using System.Collections.Generic;

namespace AnnuaireEntrepriseAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string MobilePhone { get; set; } = null!;

    public string Service { get; set; } = null!;

    public string Site { get; set; } = null!;

    public virtual Service ServiceNavigation { get; set; } = null!;

    public virtual Site SiteNavigation { get; set; } = null!;
}

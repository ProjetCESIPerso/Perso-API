using AnnuaireEntrepriseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntrepriseAPI.Database;

public partial class AnnuaireEntrepriseContext : DbContext
{
    public AnnuaireEntrepriseContext()
    {
    }

    public AnnuaireEntrepriseContext(DbContextOptions<AnnuaireEntrepriseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=AnnuaireEntreprise;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

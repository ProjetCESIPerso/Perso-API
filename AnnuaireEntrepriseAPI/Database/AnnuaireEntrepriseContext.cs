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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=AnnuaireEntreprise;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>(entity =>
        {
            //Défini la clé primaire de la BDD
            entity.HasKey(item => item.Id).HasName("PK__Service__737584F71EBE5F88");

            entity.ToTable("Service");

            entity.Property(e => e.Name).HasMaxLength(70);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Site__FF37FFF7D8B84281");

            entity.ToTable("Site");

            entity.Property(e => e.Town).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07918F8F15");

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.MobilePhone).HasMaxLength(21);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(21);
            entity.Property(e => e.Service).HasMaxLength(70);
            entity.Property(e => e.Site).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(30);

            //entity.HasOne(d => d.ServiceNavigation).WithMany(p => p.Users)
            //    .HasForeignKey(d => d.Service)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Users__Service__06CD04F7");

            //entity.HasOne(d => d.SiteNavigation).WithMany(p => p.Users)
            //    .HasForeignKey(d => d.Site)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Users__Site__07C12930");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

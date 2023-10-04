using System;
using System.Collections.Generic;
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
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=AnnuaireEntreprise;Trusted_Connection=True;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__Service__737584F700290AC8");

            entity.ToTable("Service");

            entity.Property(e => e.Name).HasMaxLength(70);
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Town).HasName("PK__Site__FF37FFF72F24B2B5");

            entity.ToTable("Site");

            entity.Property(e => e.Town).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0711ABC6D5");

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.MobilePhone).HasMaxLength(21);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(21);
            entity.Property(e => e.Service).HasMaxLength(70);
            entity.Property(e => e.Site).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.ServiceNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Service)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Service__6FE99F9F");

            entity.HasOne(d => d.SiteNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Site)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Site__70DDC3D8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

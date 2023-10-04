﻿// <auto-generated />
using AnnuaireEntrepriseAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnnuaireEntrepriseAPI.Migrations
{
    [DbContext(typeof(AnnuaireEntrepriseContext))]
    [Migration("20231004101722_Snapshot041023")]
    partial class Snapshot041023
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.Service", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Name")
                        .HasName("PK__Service__737584F71EBE5F88");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.Site", b =>
                {
                    b.Property<string>("Town")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Town")
                        .HasName("PK__Site__FF37FFF7D8B84281");

                    b.ToTable("Site", (string)null);
                });

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("ServiceNavigationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SiteNavigationTown")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC07918F8F15");

                    b.HasIndex("ServiceNavigationName");

                    b.HasIndex("SiteNavigationTown");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.User", b =>
                {
                    b.HasOne("AnnuaireEntrepriseAPI.Models.Service", "ServiceNavigation")
                        .WithMany("Users")
                        .HasForeignKey("ServiceNavigationName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnnuaireEntrepriseAPI.Models.Site", "SiteNavigation")
                        .WithMany("Users")
                        .HasForeignKey("SiteNavigationTown")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceNavigation");

                    b.Navigation("SiteNavigation");
                });

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.Service", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AnnuaireEntrepriseAPI.Models.Site", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
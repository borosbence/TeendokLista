using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TeendokLista.API.Models;

namespace TeendokLista.API.Data
{
    public partial class TeendokContext : DbContext
    {
        public TeendokContext()
        {
        }

        public TeendokContext(DbContextOptions<TeendokContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feladat> feladatok { get; set; } = null!;
        public virtual DbSet<Felhasznalo> felhasznalok { get; set; } = null!;
        public virtual DbSet<Szerepkor> szerepkorok { get; set; } = null!;
        public virtual DbSet<LoginToken> login_tokenek { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;database=teendoklista", ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Feladat>(entity =>
            {
                entity.HasOne(d => d.felhasznalo)
                    .WithMany(p => p.feladatok)
                    .HasForeignKey(d => d.felhasznalo_id)
                    .HasConstraintName("feladatok_ibfk_1");
            });

            modelBuilder.Entity<Felhasznalo>(entity =>
            {
                entity.HasOne(d => d.szerepkor)
                    .WithMany(p => p.felhasznalok)
                    .HasForeignKey(d => d.szerepkor_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("felhasznalok_ibfk_1");
            });

            modelBuilder.Entity<LoginToken>(entity =>
            {
                entity.HasOne(d => d.felhasznalo)
                    .WithMany(p => p.tokenek)
                    .HasForeignKey(d => d.felhasznalo_id)
                    .HasConstraintName("login_tokenek_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

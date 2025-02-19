using DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.DAL.DbConecction
{
    public class DbContextPrueba : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();
                var connectionString = configuration["ConfigurationDb:Prueba"];

                optionsBuilder.UseSqlServer(connectionString);
            }


        }
        public DbContextPrueba(DbContextOptions<DbContextPrueba> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__tbUser__1788CC4CBB2FA558");

                entity.ToTable("tbUser");

                entity.HasIndex(e => e.LogonName, "UQ__tbUser__536C85E4521CA2DB").IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.LogonName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => e.IdToken);

                entity.ToTable("tbUserToken");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
                entity.Property(e => e.JwtToken).IsUnicode(false);
                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.HasOne(d => d.user).WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbUserToken_tbUser");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(e => e.IdLog);

                entity.ToTable("tbErrorLog");

                entity.Property(e => e.AdditionalInformation).IsUnicode(false);
                entity.Property(e => e.AppSource)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.LogonName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Message)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deportista>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Deportis__3214EC07108D3356");

                entity.ToTable("tbDeportistas");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Intento>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intentos__3214EC070B77E0A3");

                entity.ToTable("tbIntentos");

                entity.Property(e => e.TipoIntento)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deportista).WithMany(p => p.Intentos)
                    .HasForeignKey(d => d.DeportistaId)
                    .HasConstraintName("FK__Intentos__Deport__4222D4EF");
            });

        }
    }
}

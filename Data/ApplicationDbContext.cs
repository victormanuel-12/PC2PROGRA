using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PC2proyecto.Models;
using Microsoft.AspNetCore.Identity;

namespace PC2proyecto.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  public DbSet<Mascota> Pets { get; set; }
  public DbSet<Adoptante> Adopters { get; set; }
  public DbSet<Adopcion> Adoptions { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<Mascota>()
        .HasOne(p => p.Adoption)
        .WithOne(a => a.Pet)
        .HasForeignKey<Adopcion>(a => a.PetId)
        .OnDelete(DeleteBehavior.Restrict);


    modelBuilder.Entity<Adoptante>()
        .HasMany(a => a.Adoptions)
        .WithOne(a => a.Adopter)
        .HasForeignKey(a => a.AdopterId);
  }
}

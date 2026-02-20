using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Domain.Models;

namespace ProjetoFinal.Data;

public class AppDbContext : DbContext
{
    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Equipamento>();

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Codigo)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasIndex(e => e.Codigo)
            .IsUnique();

        entity.Property(e => e.Modelo)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.Horimetro)
            .HasPrecision(18, 2);

        entity.Property(e => e.Tipo)
            .HasConversion<string>();

        entity.Property(e => e.StatusOperacional)
            .HasConversion<string>();
    }
}

using Microsoft.EntityFrameworkCore;

namespace PaneGEST_Blazor_MySQL.Models;

public partial class WwwpanetPaneGestContext : DbContext
{
    public WwwpanetPaneGestContext() { }

    public WwwpanetPaneGestContext(DbContextOptions<WwwpanetPaneGestContext> options)
        : base(options) { }

    public DbSet<Loja> Lojas => Set<Loja>();
    public DbSet<Movimento> Movimentos => Set<Movimento>();
    public DbSet<Utilizador> Utilizadores => Set<Utilizador>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Loja>(entity =>
        {
            entity.HasKey(e => e.IdLoja).HasName("PRIMARY");
            entity.ToTable("lojas");

            entity.Property(e => e.IdLoja)
                .HasColumnName("id_loja")
                .HasColumnType("int(11)");

            entity.Property(e => e.NomeLoja)
                .HasMaxLength(50)
                .HasColumnName("nome_loja");

            entity.Property(e => e.Observacoes)
                .HasMaxLength(200)
                .HasColumnName("observacoes");
        });

        modelBuilder.Entity<Movimento>(entity =>
        {
            entity.HasKey(e => e.IdMovimento).HasName("PRIMARY");
            entity.ToTable("movimentos");

            entity.Property(e => e.IdMovimento)
                .HasColumnName("id_movimento");

            entity.Property(e => e.DataMovimento)
                .HasColumnName("data_movimento");

            entity.Property(e => e.IdLoja)
                .HasColumnName("id_loja");

            entity.Property(e => e.Observacoes)
                .HasMaxLength(200)
                .HasColumnName("observacoes");

            entity.Property(e => e.ValorOff)
                .HasColumnName("valor_off");

            entity.Property(e => e.ValorOn)
                .HasColumnName("valor_on");

            entity.HasOne(d => d.IdLojaNavigation)
                .WithMany(p => p.Movimentos)
                .HasForeignKey(d => d.IdLoja)
                .HasConstraintName("FK_Movimentos_Lojas");
        });

        modelBuilder.Entity<Utilizador>(entity =>
        {
            entity.HasKey(e => e.IdUtilizador).HasName("PRIMARY");
            entity.ToTable("utilizadores");

            entity.Property(e => e.IdUtilizador)
                .HasColumnName("id_utilizador");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .HasColumnName("passwordhash");
        });
    }
}

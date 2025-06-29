using Microsoft.EntityFrameworkCore;
using ClipperStreamingApp.Domain;

public class StreamingDbContext : DbContext
{
    public StreamingDbContext(DbContextOptions<StreamingDbContext> options) : base(options)
    {
    }

    // --- DbSets para todas as entidades ---
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Assinatura> Assinaturas { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Banda> Bandas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conta>()
            .HasOne(c => c.Usuario)
            .WithOne(u => u.Conta)
            .HasForeignKey<Conta>(c => c.Id);
        
        modelBuilder.Entity<Assinatura>(entity =>
        {
            entity.HasMany(a => a.Transacoes)
                  .WithOne(t => t.Assinatura)
                  .HasForeignKey(t => t.AssinaturaId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(a => a.Plano)
                  .WithMany(p => p.Assinaturas);
            
            entity.HasOne(a => a.Conta)
                  .WithMany(c => c.Assinaturas);
        });

        modelBuilder.Entity<Musica>(entity =>
        {
            entity.HasOne(m => m.Banda)
                  .WithMany(b => b.Musicas);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasOne(p => p.Conta)
                  .WithMany(c => c.Playlists);
            
            entity.HasMany(p => p.Musicas)
                  .WithMany()
                  .UsingEntity("PlaylistMusica");
            
            entity.HasMany(p => p.Bandas)
                  .WithMany()
                  .UsingEntity("PlaylistBanda");
        });
    }
}
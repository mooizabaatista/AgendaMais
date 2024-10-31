using AgendaMais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaMais.Infra.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AgendamentoServico> AgendamentoServicos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Estabelecimento> Estabelecimentos { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgendamentoServico>()
            .HasKey(x => new { x.AgendamentoId, x.ServicoId });
        
        modelBuilder.Entity<AgendamentoServico>()
            .HasOne(x => x.Agendamento)
            .WithMany(a => a.AgendamentoServicos)
            .HasForeignKey(x => x.AgendamentoId);

        modelBuilder.Entity<AgendamentoServico>()
            .HasOne(x => x.Servico)
            .WithMany(s => s.AgendamentoServicos)
            .HasForeignKey(x => x.ServicoId);

        modelBuilder.Entity<Cliente>()
            .HasMany(x => x.Agendamentos)
            .WithOne(x => x.Cliente)
            .HasForeignKey(x => x.ClienteId);

        modelBuilder.Entity<Estabelecimento>()
            .HasMany(x => x.Clientes)
            .WithOne(x => x.Estabelecimento)
            .HasForeignKey(x => x.EstabelecimentoId);

        modelBuilder.Entity<Estabelecimento>()
            .HasMany(x => x.Servicos)
            .WithOne(x => x.Estabelecimento)
            .HasForeignKey(x => x.EstabelecimentoId);
    }

    
}
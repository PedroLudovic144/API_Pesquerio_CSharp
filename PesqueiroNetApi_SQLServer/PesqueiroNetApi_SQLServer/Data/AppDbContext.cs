using Microsoft.EntityFrameworkCore;
using PesqueiroNetApi.Entities;

namespace PesqueiroNetApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PeixeCapturado> PeixesCapturados { get; set; }
        public DbSet<Lago> Lagos { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Pesqueiro> Pesqueiros { get; set; }

        // tabelas associativas
        public DbSet<Gerencia> Gerencias { get; set; }
        public DbSet<AluguelCliente> AlugueisClientes { get; set; }
        public DbSet<CompraCliente> ComprasClientes { get; set; }
        public DbSet<PeixeCliente> PeixesClientes { get; set; }
        public DbSet<EspecieLago> EspeciesLagos { get; set; }
        public DbSet<ClienteComentario> ClientesComentarios { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entidades principais
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Funcionario>().HasKey(f => f.IdFuncionario);
            modelBuilder.Entity<Equipamento>().HasKey(e => e.IdEquipamentos);
            modelBuilder.Entity<Aluguel>().HasKey(a => a.IdAluguel);
            modelBuilder.Entity<Compra>().HasKey(c => c.IdCompra);
            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<PeixeCapturado>().HasKey(p => p.IdPeixeCapturado);
            modelBuilder.Entity<Lago>().HasKey(l => l.IdLago);
            modelBuilder.Entity<Especie>().HasKey(e => e.IdEspecie);
            modelBuilder.Entity<Comentario>().HasKey(c => c.IdComentario);
            modelBuilder.Entity<Pesqueiro>().HasKey(p => p.IdPesqueiro);

            // Tabelas associativas (N:N)
            modelBuilder.Entity<Gerencia>()
                .HasKey(g => new { g.IdFuncionario, g.IdEquipamentos });

            modelBuilder.Entity<AluguelCliente>()
                .HasKey(ac => new { ac.IdCliente, ac.IdAluguel });

            modelBuilder.Entity<CompraCliente>()
                .HasKey(cc => new { cc.IdCliente, cc.IdCompra });

            modelBuilder.Entity<PeixeCliente>()
                .HasKey(pc => new { pc.IdCliente, pc.IdPeixeCapturado });

            modelBuilder.Entity<EspecieLago>()
                .HasKey(el => new { el.IdEspecie, el.IdLago });

            modelBuilder.Entity<ClienteComentario>()
                .HasKey(cc => new { cc.IdComentario, cc.IdCliente });

            modelBuilder.Entity<Favorito>()
                .HasKey(f => new { f.IdPesqueiro, f.IdCliente });
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Pesqueiro) // Um funcionário tem um pesqueiro
                .WithMany(p => p.Funcionarios) // Um pesqueiro pode ter muitos funcionários
                .HasForeignKey(f => f.IdPesqueiro); // A chave estrangeira é IdPesqueiro
        }
    }
}

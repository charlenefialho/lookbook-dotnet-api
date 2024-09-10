using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.data
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Lookbook> Lookbooks { get; set; }

        public DbSet<LookbookProduto> LookbookProdutos { get; set; } // Adicione esta linha

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir tabela de junção entre Lookbooks e Produtos
            modelBuilder.Entity<LookbookProduto>()
                .HasKey(lp => new { lp.LookbookId, lp.ProdutoId });

            modelBuilder.Entity<LookbookProduto>()
                .HasOne(lp => lp.Lookbook)
                .WithMany(l => l.LookbookProdutos)
                .HasForeignKey(lp => lp.LookbookId);

            modelBuilder.Entity<LookbookProduto>()
                .HasOne(lp => lp.Produto)
                .WithMany(p => p.LookbookProdutos)
                .HasForeignKey(lp => lp.ProdutoId);

            // Populando dados iniciais
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Camiseta Branca", Preco = 29.99, Tags = new List<string> { "Casual", "Verão" } },
                new Produto { Id = 2, Nome = "Calça Jeans", Preco = 79.99, Tags = new List<string> { "Outono", "Durável" } },
                new Produto { Id = 3, Nome = "Jaqueta de Couro", Preco = 199.99, Tags = new List<string> { "Inverno", "Estiloso" } }
            );

            modelBuilder.Entity<Lookbook>().HasData(
                new Lookbook { Id = 1, Nome = "Look de Verão" },
                new Lookbook { Id = 2, Nome = "Look de Inverno" }
            );

            modelBuilder.Entity<LookbookProduto>().HasData(
                new LookbookProduto { LookbookId = 1, ProdutoId = 1 }, // Camiseta Branca no Look de Verão
                new LookbookProduto { LookbookId = 1, ProdutoId = 2 }, // Calça Jeans no Look de Verão
                new LookbookProduto { LookbookId = 2, ProdutoId = 3 }  // Jaqueta de Couro no Look de Inverno
            );
        }

    }
}
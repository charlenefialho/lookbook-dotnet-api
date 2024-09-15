using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lookbook> Lookbooks { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lookbook>()
                .HasMany(l => l.Produtos)
                .WithMany()
                .UsingEntity(j => j.ToTable("LookbookProduto"));
        }

    }

}
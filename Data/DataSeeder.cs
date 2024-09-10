using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.data
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Lookbooks.Any())
            {
                var lookbooks = new List<Lookbook>
            {
                new Lookbook { Nome = "Lookbook 1", Descricao = "Descrição do Lookbook 1", DataCriacao = DateTime.Now },
                new Lookbook { Nome = "Lookbook 2", Descricao = "Descrição do Lookbook 2", DataCriacao = DateTime.Now }
            };
                context.Lookbooks.AddRange(lookbooks);
                context.SaveChanges();
            }

            if (!context.Produtos.Any())
            {
                var produtos = new List<Produto>
            {
                new Produto { Nome = "Camisa A", Categoria = "Camiseta" },
                new Produto { Nome = "Sapato B", Categoria = "sapato" }
            };
                context.Produtos.AddRange(produtos);
                context.SaveChanges();

                // Adiciona produtos a um lookbook
                var lookbook1 = context.Lookbooks.FirstOrDefault(l => l.Nome == "Lookbook 1");
                var produto1 = context.Produtos.FirstOrDefault(p => p.Nome == "Camisa A");
                if (lookbook1 != null && produto1 != null)
                {
                    lookbook1.Produtos.Add(produto1);
                    context.SaveChanges();
                }
            }
        }
    }
}
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.data
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Verifica se já existem Lookbooks no banco de dados
            if (context.Lookbooks.Count() == 0)
            {
                var lookbooks = new List<Lookbook>
                {
                    new Lookbook { Nome = "Verão 2024", Descricao = "Coleção de verão com as últimas tendências", DataCriacao = DateTime.Now },
                    new Lookbook { Nome = "Outono Elegante", Descricao = "Estilos sofisticados para a estação do outono", DataCriacao = DateTime.Now },
                    new Lookbook { Nome = "Primavera Fresca", Descricao = "Roupas leves e alegres para a primavera", DataCriacao = DateTime.Now },
                    new Lookbook { Nome = "Inverno Aconchegante", Descricao = "Peças quentes e confortáveis para o inverno", DataCriacao = DateTime.Now },
                    new Lookbook { Nome = "Moda Casual", Descricao = "Lookbook com opções casuais e confortáveis para o dia a dia", DataCriacao = DateTime.Now }
                };
                context.Lookbooks.AddRange(lookbooks);
                context.SaveChanges();
            }

            // Verifica se já existem Produtos no banco de dados
            if (context.Produtos.Count() == 0)
            {
                var produtos = new List<Produto>
                {
                    new Produto { Nome = "Camisa Polo Azul", Categoria = "Camiseta" },
                    new Produto { Nome = "Calça Jeans Escura", Categoria = "Calças" },
                    new Produto { Nome = "Tênis Branco Casual", Categoria = "Calçados" },
                    new Produto { Nome = "Blazer Preto Slim", Categoria = "Jaquetas" },
                    new Produto { Nome = "Vestido Floral", Categoria = "Roupas" }
                };
                context.Produtos.AddRange(produtos);
                context.SaveChanges();
            }

            // Associa produtos a lookbooks
            /*
            var lookbookVerano = context.Lookbooks.FirstOrDefault(l => l.Nome == "Verão 2024");
            var lookbookOutono = context.Lookbooks.FirstOrDefault(l => l.Nome == "Outono Elegante");
            var produtoCamisaPolo = context.Produtos.FirstOrDefault(p => p.Nome == "Camisa Polo Azul");
            var produtoCalcaJeans = context.Produtos.FirstOrDefault(p => p.Nome == "Calça Jeans Escura");
            var produtoTenisBranco = context.Produtos.FirstOrDefault(p => p.Nome == "Tênis Branco Casual");
            var produtoBlazerPreto = context.Produtos.FirstOrDefault(p => p.Nome == "Blazer Preto Slim");
            var produtoVestidoFloral = context.Produtos.FirstOrDefault(p => p.Nome == "Vestido Floral");

            if (lookbookVerano != null && produtoCamisaPolo != null && produtoTenisBranco != null)
            {
                lookbookVerano.Produtos.Add(produtoCamisaPolo);
                lookbookVerano.Produtos.Add(produtoTenisBranco);
            }

            if (lookbookOutono != null && produtoCalcaJeans != null && produtoBlazerPreto != null)
            {
                lookbookOutono.Produtos.Add(produtoCalcaJeans);
                lookbookOutono.Produtos.Add(produtoBlazerPreto);
            }*/

            // Salva as associações
            context.SaveChanges();
        }
    }
}

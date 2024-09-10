using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lookbook_dotnet_api.models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        public ICollection<LookbookProduto> LookbookProdutos { get; set; } = new List<LookbookProduto>();

        public List<string> Tags { get; set; } // Tags como lista de strings
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;


namespace lookbook_dotnet_api.models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        public ICollection<LookbookProduto> LookbookProdutos { get; set; } = new List<LookbookProduto>();

        public List<string> Tags { get; set; } // Tags como lista de strings
    }
}
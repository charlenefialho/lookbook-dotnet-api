using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lookbook_dotnet_api.models
{
    public class Lookbook
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lookbook_dotnet_api.models
{
    public class LookbookProduto
    {
        public int LookbookId { get; set; }
        public int ProdutoId { get; set; }

        public Lookbook Lookbook { get; set; }
        public Produto Produto { get; set; }
    }
}
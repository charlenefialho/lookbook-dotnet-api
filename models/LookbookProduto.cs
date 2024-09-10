using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lookbook_dotnet_api.models
{
    public class LookbookProduto
    {
        [Key, Column(Order = 0)]
        public int LookbookId { get; set; }

        [Key, Column(Order = 1)]
        public int ProdutoId { get; set; }

        public Lookbook Lookbook { get; set; }
        public Produto Produto { get; set; }
    }
}
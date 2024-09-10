using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lookbook_dotnet_api.DTO
{
    public class LookbookCreateDto
    {
        public string Nome { get; set; }
        public List<int> ProdutoIds { get; set; }
    }
}
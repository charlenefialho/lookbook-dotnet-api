using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<Produto> Create(Produto produto);
        Task<Produto> Update(int id, Produto produto);
        Task<bool> Delete(int id);

    }
}
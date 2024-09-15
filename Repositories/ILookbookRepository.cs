using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.Repositories
{
    public interface ILookbookRepository : IRepository<Lookbook>
    {
        Task<IEnumerable<Lookbook>> GetLookbooksWithProdutosAsync();
    }

}
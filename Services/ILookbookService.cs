using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.Services
{
    public interface ILookbookService
    {
        Task<IEnumerable<Lookbook>> GetAllLookbooksAsync();
        Task<Lookbook> CreateLookbookAsync(Lookbook lookbook);
        Task UpdateLookbookAsync(int id, Lookbook lookbook);
        Task DeleteLookbookAsync(int id);
    }

}

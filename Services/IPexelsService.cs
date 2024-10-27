using lookbook_dotnet_api.models;
using PexelsDotNetSDK.Models;

namespace lookbook_dotnet_api.Services
{
    public interface IPexelsService
    {
        Task<PhotoPage> SearchPhotosAsync(string query);
    }
}

using lookbook_dotnet_api.models;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;

namespace lookbook_dotnet_api.Services
{

    public class PexelsService : IPexelsService
    {
        private readonly PexelsClient _pexelsClient;

        public PexelsService(string apiKey)
        {
            _pexelsClient = new PexelsClient(apiKey);
        }

        public async Task<PhotoPage> SearchPhotosAsync(string query)
        {
            return await _pexelsClient.SearchPhotosAsync(query);
        }
    }
}

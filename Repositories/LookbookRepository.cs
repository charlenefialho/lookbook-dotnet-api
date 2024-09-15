using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using lookbook_dotnet_api.data;
using lookbook_dotnet_api.models;

namespace lookbook_dotnet_api.Repositories
{
    public class LookbookRepository : Repository<Lookbook>, ILookbookRepository
    {
        private readonly ApplicationDbContext _context;

        public LookbookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lookbook>> GetLookbooksWithProdutosAsync()
        {
            return await _context.Lookbooks
                .Include(l => l.Produtos)
                .ToListAsync();
        }
    }

}
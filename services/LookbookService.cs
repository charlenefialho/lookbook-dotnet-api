using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.data;
using Microsoft.EntityFrameworkCore;

namespace lookbook_dotnet_api.services
{
    public class LookbookService
    {
        private readonly OracleDbContext _context;

        public LookbookService(OracleDbContext context)
        {
            _context = context;
        }

        // Listar todos os lookbooks
        public async Task<IEnumerable<Lookbook>> GetAll()
        {
            return await _context.Lookbooks.Include(lb => lb.LookbookProdutos).ThenInclude(lp => lp.Produto).ToListAsync();
        }

        // Obter lookbook por ID
        public async Task<Lookbook> GetById(int id)
        {
            return await _context.Lookbooks
                .Include(lb => lb.LookbookProdutos)
                .ThenInclude(lp => lp.Produto)
                .FirstOrDefaultAsync(lb => lb.Id == id);
        }

        // Criar novo lookbook
        public async Task Create(Lookbook lookbook)
        {
            _context.Lookbooks.Add(lookbook);
            await _context.SaveChangesAsync();

            if (lookbook.LookbookProdutos != null)
            {
                foreach (var produtoId in lookbook.LookbookProdutos.Select(lp => lp.ProdutoId).Distinct())
                {
                    var lookbookProduto = new LookbookProduto
                    {
                        LookbookId = lookbook.Id,
                        ProdutoId = produtoId
                    };
                    _context.LookbookProdutos.Add(lookbookProduto);
                }

                await _context.SaveChangesAsync();
            }
        }

        // Atualizar lookbook existente
        public async Task Update(Lookbook lookbook)
        {
            _context.Entry(lookbook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Deletar lookbook por ID
        public async Task Delete(int id)
        {
            var lookbook = await _context.Lookbooks.FindAsync(id);
            if (lookbook != null)
            {
                _context.Lookbooks.Remove(lookbook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
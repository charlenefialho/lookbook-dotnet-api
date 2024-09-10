using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.data;
using Microsoft.EntityFrameworkCore;

namespace lookbook_dotnet_api.services
{
    public class ProdutoService
    {
        private readonly OracleDbContext _context;

        public ProdutoService(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task Create(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }

}
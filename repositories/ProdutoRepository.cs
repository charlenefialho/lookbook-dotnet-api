using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lookbook_dotnet_api.interfaces;
using lookbook_dotnet_api.models;
using lookbook_dotnet_api.data;

namespace lookbook_dotnet_api.services
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly OracleDbContext _context;

        public ProdutoRepository(OracleDbContext context)
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

        public async Task<Produto> Create(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Update(int id, Produto produto)
        {
            var existingProduto = await _context.Produtos.FindAsync(id);
            if (existingProduto == null)
                return null;

            existingProduto.Nome = produto.Nome;
            existingProduto.Preco = produto.Preco;
            existingProduto.Tags = produto.Tags;

            await _context.SaveChangesAsync();
            return existingProduto;
        }

        public async Task<bool> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
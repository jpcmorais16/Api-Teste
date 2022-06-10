using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDbContext _context;
        ICategoriaRepository _categoriaRepository;//pq nn passar no construtor??
        IProdutoRepository _produtoRepository;
        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
            
            
        }
        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

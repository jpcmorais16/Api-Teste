using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
         
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
    }
}

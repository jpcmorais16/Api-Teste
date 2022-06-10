using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public float Price { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}

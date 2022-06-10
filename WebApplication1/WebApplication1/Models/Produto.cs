using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
	public class Produto
	{
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public float Price { get; set; }

        public DateTime CreationdDate { get; set; }

        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
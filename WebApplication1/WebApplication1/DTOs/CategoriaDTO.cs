﻿using System.Collections.Generic;

namespace WebApplication1.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }

        public ICollection<ProdutoDTO> Produtos { get; set; }
    }
}

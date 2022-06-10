using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebApplication1.Models 
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        
        }

        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<Produto> Produtos { get; set; }
        
    }


}



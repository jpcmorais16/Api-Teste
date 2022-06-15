using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models 
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        
        }

        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Nome obrigatorio")]
        public string Nome { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<Produto> Produtos { get; set; }
        
    }


}



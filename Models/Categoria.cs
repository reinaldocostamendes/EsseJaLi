using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsseJaLi.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LivrosLeitores> Livros { get; set; }

        public virtual ICollection<Premio> Premios { get; set; }
    }
}
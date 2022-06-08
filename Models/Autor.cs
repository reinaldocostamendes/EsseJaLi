using System;
using System.Collections.Generic;

namespace EsseJaLi.Models
{
    public class Autor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public virtual ICollection<LivrosLeitores> Livros { get; set; }
    }
}

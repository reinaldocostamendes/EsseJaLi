using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsseJaLi.Models
{
    public class LivrosLeitores
    {
        public Guid Id { get; set; }
        [ForeignKey("Autor")]
        public Guid IdAutor { get; set; }
        [ForeignKey("Categoria")]
        public Guid IdCategoria { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public int Paginas { get; set; }
        public virtual Autor Autor { get; set; }    
        public virtual Categoria Categoria { get; set; }  
        public virtual ICollection<LivroLeitor> LivroLeitores { get; set; }
    }
}

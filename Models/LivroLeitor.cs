using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsseJaLi.Models
{
    public class LivroLeitor
    {
        public Guid Id { get; set; }

        public Guid IdCategoria { get; set; }

        [ForeignKey("Livro")]
        public Guid IdLivro { get; set; }

        [ForeignKey("Leitor")]
        public string IdLeitor { get; set; }

        public virtual Leitor Leitor { get; set; }
        public virtual LivrosLeitores Livro { get; set; }
    }
}
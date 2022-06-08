using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsseJaLi.Models
{
    public class Premio
    {
        public Guid Id { get; set; }

        [ForeignKey("Categoria")]
        public Guid IdCategoria { get; set; }

        [ForeignKey("Leitor")]
        public string IdLeitor { get; set; }

        public virtual Leitor Leitor { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
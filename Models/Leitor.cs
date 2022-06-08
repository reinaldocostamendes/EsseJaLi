using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsseJaLi.Models
{
    public class Leitor : IdentityUser
    {
        /*[Column("LeitorID")]
        public Guid Id { get; set; }*/

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Pontuacao")]
        public int Pontuacao { get; set; }

        public string CategoriaLeitura { get; set; } = "Geral";
        public virtual ICollection<LivroLeitor> LivrosLeitores { get; set; }

        public virtual ICollection<Premio> Premios { get; set; }
    }
}
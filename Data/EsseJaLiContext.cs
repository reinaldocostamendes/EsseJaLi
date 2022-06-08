using EsseJaLi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EsseJaLi.Data
{
    public class EsseJaLiContext : IdentityDbContext<IdentityUser>
    {
        public EsseJaLiContext(DbContextOptions<EsseJaLiContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<LivrosLeitores> Livros { get; set; }
        public DbSet<Leitor> Leitores { get; set; }
        public DbSet<Premio> Premios { get; set; }

        public DbSet<LivroLeitor> LivrosLeitores { get; set; }
        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
              modelBuilder.Entity<Livro>().HasOne<Categoria>(o => o.Categoria).WithMany(p => p.Livros)
                  .HasForeignKey(e => e.IdCategoria).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

              modelBuilder.Entity<Livro>().HasOne<Autor>(o => o.Autor).WithMany(p => p.Livros)
                  .HasForeignKey(e => e.IdAutor).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

              modelBuilder.Entity<LivroLeitor>().HasOne<Leitor>(o => o.Leitor).WithMany(p => p.LivrosLeitores)
               .HasForeignKey(e => e.Id).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

              modelBuilder.Entity<LivroLeitor>().HasOne<Livro>(o => o.Livro).WithMany(p => p.LivroLeitores)
             .HasForeignKey(e => e.IdLivro).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

              modelBuilder.Entity<Leitor>().ToTable("IdentityUser").HasKey(t => t.Id);

              base.OnModelCreating(modelBuilder);
          }

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
              if (!optionsBuilder.IsConfigured)
              {
                  optionsBuilder.UseSqlServer(ObterStringConexao());
                  base.OnConfiguring(optionsBuilder);
              }
          }

          public string ObterStringConexao()
          {
              string strcon = "Server=Reinaldo\\SQLEXPRESS; Database = EsseJaLiDB; Integrated Security = True";
              return strcon;
          }*/
    }
}
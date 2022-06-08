using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsseJaLi.Data;
using EsseJaLi.Models;
using Microsoft.AspNetCore.Authorization;

namespace EsseJaLi.Controllers
{
    public class LivrosController : Controller
    {
        private readonly EsseJaLiContext _context;

        public LivrosController(EsseJaLiContext context)
        {
            _context = context;
        }

        // GET: Livroes
        public async Task<IActionResult> Index()
        {
            var esseJaLiContext = _context.Livros.Include(l => l.Autor).Include(l => l.Categoria);
            return View(await esseJaLiContext.ToListAsync());
        }

        // GET: Livroes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livroes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_context.Autores, "Id", "Name");
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Name");
            return View();
        }

        // POST: Livroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutor,IdCategoria,Titulo,Conteudo,Paginas")] LivrosLeitores livro)
        {
            if (ModelState.IsValid)
            {
                livro.Id = Guid.NewGuid();
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "Id", "Id", livro.IdAutor);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Id", livro.IdCategoria);
            return View(livro);
        }

        // GET: Livroes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "Id", "Name", livro.IdAutor);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Id", "Name" +
                "", livro.IdCategoria);
            return View(livro);
        }

        // POST: Livroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdAutor,IdCategoria,Titulo,Conteudo,Paginas")] LivrosLeitores livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAutor"] = new SelectList(_context.Autores, "Name", "Id", livro.IdAutor);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Name", "Id", livro.IdCategoria);
            return View(livro);
        }

        // GET: Livroes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livroes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var livro = await _context.Livros.FindAsync(id);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(Guid id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
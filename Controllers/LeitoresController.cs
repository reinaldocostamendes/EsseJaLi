using EsseJaLi.Data;
using EsseJaLi.Models;
using EsseJaLi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsseJaLi.Controllers
{
    public class LeitoresController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EsseJaLiContext _context;

        public LeitoresController(SignInManager<IdentityUser> userHelper, UserManager<IdentityUser> userManager, EsseJaLiContext context)
        {
            _signInManager = userHelper;
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginLeitorViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginLeitorViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user,
                    loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Livros");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!!");
            return View(loginVM);
        }//

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var lista = new List<LeitorViewModel>();
            var leitores = await _context.Leitores.ToListAsync();
            var str_tfu = "";
            foreach (var l in leitores)
            {
                var premios = await _context.Premios.Where(p => p.IdLeitor == l.Id).ToListAsync();
                foreach (var p in premios)
                {
                    var categoria = await _context.Categorias.Where(c => c.Id == p.IdCategoria).FirstOrDefaultAsync();
                    str_tfu += " - " + categoria.Name;
                }

                var lvm = new LeitorViewModel()
                {
                    Nome = l.Nome,
                    Pontuacao = l.Pontuacao,
                    CatergoriLeitura = l.CategoriaLeitura,
                    LeitorTrofeus = " [ " + str_tfu + " ] "
                };
                lista.Add(lvm);
                str_tfu = "";
            }

            return View(lista.OrderByDescending(l => l.Pontuacao));
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterLeitorViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new Leitor
                {
                    UserName = registroVM.Username,
                    Nome = registroVM.FirstName + " " +
                    "" + registroVM.LastName,
                    Email = registroVM.Username,
                    PasswordHash = registroVM.Password,
                };
                if (!registroVM.Password.Equals(registroVM.Confirm))
                {
                    return View();
                }
                var result = await _userManager.CreateAsync(user, registroVM.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //  await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }
            return View(registroVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // HttpContext.Session.Clear();
            // HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoLido(LivrosLeitores livro)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            var livro_leitor = _context.LivrosLeitores.Where(l => l.IdLeitor == user.Id && l.IdLivro == livro.Id).FirstOrDefault();
            if (livro_leitor == null)
            {
                _context.LivrosLeitores.Add(new LivroLeitor()
                {
                    IdCategoria = livro.IdCategoria,
                    IdLivro = livro.Id,
                    IdLeitor = user.Id
                });
                await _context.SaveChangesAsync();

                int totalPages = livro.Paginas;
                int pontuacao = totalPages / 72;
                var categoria = _context.Categorias.Where(c => c.Id == livro.IdCategoria).FirstOrDefault();

                var leitor = _context.Leitores.Where(l => l.Id == user.Id).FirstOrDefault();

                leitor.Pontuacao = leitor.Pontuacao + pontuacao;
                _context.Leitores.Update(leitor);
                await _context.SaveChangesAsync();
                var lv = _context.LivrosLeitores.Where(lt => lt.IdLeitor == user.Id && lt.IdCategoria == livro.IdCategoria).ToList();

                if (lv.Count() >= 5)
                {
                    var ctl = await _context.Premios.Where(p => p.IdCategoria == livro.IdCategoria && p.IdLeitor == user.Id).FirstOrDefaultAsync();
                    if (ctl == null)
                    {
                        _context.Add(new Premio()
                        {
                            IdCategoria = livro.IdCategoria,
                            IdLeitor = user.Id
                        });
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index", "Livros");
        }
    }
}
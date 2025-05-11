using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFSD_WebMovies.Data;
using ProyectoFSD_WebMovies.Models;

namespace ProyectoFSD_WebMovies.Controllers
{
    public class GenerosController : Controller
    {
        private readonly AppDbContext _context;

        public GenerosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Generos
       
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 4;

            var generos = from g in _context.Generos
                          select g;

            if (!string.IsNullOrEmpty(searchString))
            {
                generos = generos.Where(g => g.Nombre.Contains(searchString));
            }

            generos = generos.OrderBy(g => g.Nombre);

            int totalGeneros = await generos.CountAsync();

            var generos2 = await generos
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalGeneros / (double)pageSize);
            ViewData["CurrentFilter"] = searchString;

            return View(generos2);
        }


        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Generos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
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
            return View(genero);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFSD_WebMovies.Data;
using ProyectoFSD_WebMovies.Models;

namespace ProyectoFSD_WebMovies.Controllers
{
    public class ActoresController : Controller
    {
        private readonly AppDbContext _context;

        public ActoresController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Actores
        public async Task<IActionResult> Index(string searchString)
        {
            var actores = from a in _context.Actores
                          select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                actores = actores.Where(a => a.Nombre.Contains(searchString));
            }

            actores = actores
                .GroupBy(a => a.Nombre)
                .Select(g => g.First())
                .AsQueryable();

            return View(await actores.ToListAsync());
        }


        // GET: Actores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actores/Create
        public IActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre");
            ViewBag.DirectorId = new SelectList(_context.Directores, "Id", "Nombre");
            return View();
        }

        // POST: Actores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actor actor, IFormFile ImagenArchivo)
        {
            if (ModelState.IsValid)
            {
                // Manejo de imagen
                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImagenArchivo.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagenArchivo.CopyToAsync(fileStream);
                    }

                    actor.ImagenRuta = "/imagenes/" + uniqueFileName;
                }

                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(actor);
        }
    

        // GET: Actores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actores.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actorEditado, IFormFile ImagenArchivo)
        {
            if (id != actorEditado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var actorOriginal = await _context.Actores.FindAsync(id);
                    if (actorOriginal == null)
                        return NotFound();

                    actorOriginal.Nombre = actorEditado.Nombre;
                    actorOriginal.Biografia = actorEditado.Biografia;
                    actorOriginal.FechaNacimiento = actorEditado.FechaNacimiento;

                    if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                    {
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await ImagenArchivo.CopyToAsync(stream);
                        }

                        actorOriginal.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Actores.Any(e => e.Id == actorEditado.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(actorEditado);
        }





        // GET: Actores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actores.FindAsync(id);
            if (actor != null)
            {
                _context.Actores.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actores.Any(e => e.Id == id);
        }
    }
}

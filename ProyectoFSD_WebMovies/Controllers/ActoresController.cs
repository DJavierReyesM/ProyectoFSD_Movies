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
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 4;

            var actores = _context.Actores.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                actores = actores.Where(a => a.Nombre.Contains(searchString));
            }

            actores = actores.OrderBy(a => a.Nombre);

            int totalActores = await actores.CountAsync();

            var actores2 = await actores
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalActores / (double)pageSize);
            ViewData["CurrentFilter"] = searchString;

            return View(actores2);
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
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                // Manejo de imagen
                if (actor.ImagenArchivo != null && actor.ImagenArchivo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(actor.ImagenArchivo.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await actor.ImagenArchivo.CopyToAsync(fileStream);
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
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            // Recuperar libro anterior de la base de datos
            var actorExistente = await _context.Actores.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            if (actorExistente == null)
                return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    if (actor.ImagenArchivo != null && actor.ImagenArchivo.Length > 0)
                    {
                        // Eliminar la imagen anterior del servidor
                        if (!string.IsNullOrEmpty(actorExistente.ImagenRuta))
                        {
                            var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", actorExistente.ImagenRuta.TrimStart('/'));
                            if (System.IO.File.Exists(rutaAnterior))
                            {
                                System.IO.File.Delete(rutaAnterior);
                            }
                        }

                        //Guardar nueva imagen
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(actor.ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await actor.ImagenArchivo.CopyToAsync(stream);
                        }

                        actor.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }
                    else
                    {
                        // Recuperar la imagen anterior de la base de datos
                        actor.ImagenRuta = actorExistente.ImagenRuta;
                    }

                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
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

            ViewData["Id"] = new SelectList(_context.Actores, "Id", "Nombre", actor.Id);
            return View(actor);
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
                if (!string.IsNullOrEmpty(actor.ImagenRuta)) 
                {
                    var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", actor.ImagenRuta.TrimStart('/'));

                    if (System.IO.File.Exists(rutaCompleta)) 
                    {
                        System.IO.File.Delete(rutaCompleta);
                    }
                }
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

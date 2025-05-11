using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFSD_WebMovies.Data;
using ProyectoFSD_WebMovies.Models;

namespace ProyectoFSD_WebMovies.Controllers
{
    public class DirectoresController : Controller
    {
        private readonly AppDbContext _context;

        public DirectoresController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 4;

            var directores = from d in _context.Directores
                             select d;

            if (!string.IsNullOrEmpty(searchString))
            {
                directores = directores.Where(d => d.Nombre.Contains(searchString));
            }

            directores = directores.OrderBy(d => d.Nombre);

            int totalDirectores = await directores.CountAsync();

            var directores2 = await directores
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalDirectores / (double)pageSize);
            ViewData["CurrentFilter"] = searchString;

            return View(directores2);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var director = await _context.Directores.FirstOrDefaultAsync(m => m.Id == id);
            if (director == null) return NotFound();

            return View(director);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Director director)
        {
            if (ModelState.IsValid)
            {
                if (director.ImagenArchivo != null && director.ImagenArchivo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(director.ImagenArchivo.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await director.ImagenArchivo.CopyToAsync(fileStream);
                    }

                    director.ImagenRuta = "/imagenes/" + uniqueFileName;
                }

                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(director);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var director = await _context.Directores.FindAsync(id);
            if (director == null) return NotFound();

            return View(director);
        }
        // POST: Directores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            // Recuperar director anterior de la base de datos
            var directorExistente = await _context.Directores.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (directorExistente == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (director.ImagenArchivo != null && director.ImagenArchivo.Length > 0)
                    {
                        // Eliminar la imagen anterior del servidor
                        if (!string.IsNullOrEmpty(directorExistente.ImagenRuta))
                        {
                            var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", directorExistente.ImagenRuta.TrimStart('/'));
                            if (System.IO.File.Exists(rutaAnterior))
                            {
                                System.IO.File.Delete(rutaAnterior);
                            }
                        }

                        // Guardar nueva imagen
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(director.ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await director.ImagenArchivo.CopyToAsync(stream);
                        }

                        director.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }
                    else
                    {
                        // Recuperar la imagen anterior de la base de datos
                        director.ImagenRuta = directorExistente.ImagenRuta;
                    }

                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
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

            ViewData["Id"] = new SelectList(_context.Directores, "Id", "Nombre", director.Id);
            return View(director);
        }

        // Método auxiliar para verificar si existe el director
        private bool DirectorExists(int id)
        {
            return _context.Directores.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var director = await _context.Directores.FirstOrDefaultAsync(m => m.Id == id);
            if (director == null) return NotFound();

            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directores.FindAsync(id);
            if (director != null)
            {

                if (!string.IsNullOrEmpty(director.ImagenRuta))
                {
                    var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", director.ImagenRuta.TrimStart('/'));

                    if (System.IO.File.Exists(rutaCompleta))
                    {
                        System.IO.File.Delete(rutaCompleta);
                    }
                }
                _context.Directores.Remove(director);
                
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

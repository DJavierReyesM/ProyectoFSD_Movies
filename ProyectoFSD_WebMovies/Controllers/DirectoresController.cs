using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(string searchString)
        {
            var directores = from d in _context.Directores
                             select d;

            if (!string.IsNullOrEmpty(searchString))
            {
                directores = directores.Where(d => d.Nombre.Contains(searchString));
            }

            directores = directores
                .GroupBy(d => d.Nombre)
                .Select(g => g.First())
                .AsQueryable();

            return View(await directores.ToListAsync());
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
        public async Task<IActionResult> Create(Director director, IFormFile ImagenArchivo)
        {
            if (ModelState.IsValid)
            {
                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImagenArchivo.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagenArchivo.CopyToAsync(fileStream);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Director directorEditado, IFormFile ImagenArchivo)
        {
            if (id != directorEditado.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var directorOriginal = await _context.Directores.FindAsync(id);
                    if (directorOriginal == null) return NotFound();

                    directorOriginal.Nombre = directorEditado.Nombre;
                    directorOriginal.Nacionalidad = directorEditado.Nacionalidad;
                    directorOriginal.FechaNacimiento = directorEditado.FechaNacimiento;

                    if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                    {
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await ImagenArchivo.CopyToAsync(stream);
                        }

                        directorOriginal.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Directores.Any(e => e.Id == directorEditado.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(directorEditado);
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
                _context.Directores.Remove(director);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

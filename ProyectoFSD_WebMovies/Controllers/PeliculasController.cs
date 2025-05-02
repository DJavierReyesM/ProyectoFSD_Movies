using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFSD_WebMovies.Data;
using ProyectoFSD_WebMovies.Models;

namespace ProyectoFSD_WebMovies.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly AppDbContext _context;

        public PeliculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(string searchString)
        {
            var peliculas = from p in _context.Peliculas.Include(p => p.Genero).Include(p => p.Director)
                            select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(searchString));
            }

            peliculas = peliculas
                .GroupBy(p => p.Titulo)
                .Select(g => g.First())
                .AsQueryable();

            return View(await peliculas.ToListAsync());
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre");
            ViewBag.DirectorId = new SelectList(_context.Directores, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                if (pelicula.ImagenArchivo != null && pelicula.ImagenArchivo.Length > 0)
                {
                    var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var nombreUnico = Guid.NewGuid().ToString() + "_" + Path.GetFileName(pelicula.ImagenArchivo.FileName);
                    var rutaArchivo = Path.Combine(carpeta, nombreUnico);

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await pelicula.ImagenArchivo.CopyToAsync(stream);
                    }

                    pelicula.ImagenRuta = "/imagenes/" + nombreUnico;
                }

                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewBag.DirectorId = new SelectList(_context.Directores, "Id", "Nombre", pelicula.DirectorId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null) return NotFound();

            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewBag.DirectorId = new SelectList(_context.Directores, "Id", "Nombre", pelicula.DirectorId);

            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pelicula pelicula)
        {
            if (id != pelicula.Id) return NotFound();

            // Recuperar director anterior de la base de datos
            var peliculaExistente = await _context.Peliculas.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (peliculaExistente == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (pelicula.ImagenArchivo != null && pelicula.ImagenArchivo.Length > 0)
                    {
                        // Eliminar la imagen anterior del servidor
                        if (!string.IsNullOrEmpty(pelicula.ImagenRuta))
                        {
                            var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pelicula.ImagenRuta.TrimStart('/'));
                            if (System.IO.File.Exists(rutaAnterior))
                            {
                                System.IO.File.Delete(rutaAnterior);
                            }
                        }

                        // Guardar nueva imagen
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(pelicula.ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await pelicula.ImagenArchivo.CopyToAsync(stream);
                        }

                        pelicula.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }
                    else
                    {
                        // Recuperar la imagen anterior de la base de datos
                        pelicula.ImagenRuta = peliculaExistente.ImagenRuta;
                    }

                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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

            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Nombre", pelicula.DirectorId);
            return View(pelicula);
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pelicula == null) return NotFound();

            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pelicula == null) return NotFound();

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

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
        public async Task<IActionResult> Create(Pelicula pelicula, IFormFile ImagenArchivo)
        {
            if (ModelState.IsValid)
            {
                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var nombreUnico = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImagenArchivo.FileName);
                    var rutaArchivo = Path.Combine(carpeta, nombreUnico);

                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await ImagenArchivo.CopyToAsync(stream);
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
        public async Task<IActionResult> Edit(int id, Pelicula peliculaEditada, IFormFile ImagenArchivo)
        {
            if (id != peliculaEditada.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var peliculaOriginal = await _context.Peliculas.FindAsync(id);
                    if (peliculaOriginal == null) return NotFound();

                    peliculaOriginal.Titulo = peliculaEditada.Titulo;
                    peliculaOriginal.Sinopsis = peliculaEditada.Sinopsis;
                    peliculaOriginal.Duracion = peliculaEditada.Duracion;
                    peliculaOriginal.FechaEstreno = peliculaEditada.FechaEstreno;
                    peliculaOriginal.GeneroId = peliculaEditada.GeneroId;
                    peliculaOriginal.DirectorId = peliculaEditada.DirectorId;

                    if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                    {
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ImagenArchivo.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                            await ImagenArchivo.CopyToAsync(stream);
                        }

                        peliculaOriginal.ImagenRuta = "/imagenes/" + nombreArchivo;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Peliculas.Any(e => e.Id == peliculaEditada.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre", peliculaEditada.GeneroId);
            ViewBag.DirectorId = new SelectList(_context.Directores, "Id", "Nombre", peliculaEditada.DirectorId);
            return View(peliculaEditada);
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

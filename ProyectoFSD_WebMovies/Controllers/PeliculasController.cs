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
                var peliculas = from p in _context.Peliculas
                                .Include(p => p.Director)
                                .Include(p => p.Genero)
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


            // GET: Peliculas/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pelicula = await _context.Peliculas
                    .Include(p => p.Director)
                    .Include(p => p.Genero)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (pelicula == null)
                {
                    return NotFound();
                }

                return View(pelicula);
            }

            // GET: Peliculas/Create
            public IActionResult Create()
            {
                ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Id");
                ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id");
                return View();
            }

        // POST: Peliculas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula, IFormFile ImagenArchivo)
        {
            if (ModelState.IsValid)
            {
                // Manejar la carga de imágenes
                if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImagenArchivo.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagenArchivo.CopyToAsync(fileStream);
                    }

                    pelicula.ImagenRuta = "/imagenes/" + uniqueFileName; // Ruta accesible desde el cliente
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
                if (id == null)
                {
                    return NotFound();
                }

                var pelicula = await _context.Peliculas.FindAsync(id);
                if (pelicula == null)
                {
                    return NotFound();
                }
                ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Id", pelicula.DirectorId);
                ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Id", pelicula.GeneroId);
                return View(pelicula);
            }

            // POST: Peliculas/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Pelicula pelicula, IFormFile ImagenArchivo) 
            {
                if (id != pelicula.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (ImagenArchivo != null && ImagenArchivo.Length > 0)
                        {
                            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(ImagenArchivo.FileName);
                            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                            using (var stream = new FileStream(ruta, FileMode.Create))
                            {
                                await ImagenArchivo.CopyToAsync(stream);
                            }

                            pelicula.ImagenRuta = "/imagenes/" + nombreArchivo;
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
                return View(pelicula);
            }
            // GET: Peliculas/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var pelicula = await _context.Peliculas
                    .Include(p => p.Director)
                    .Include(p => p.Genero)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (pelicula == null)
                {
                    return NotFound();
                }

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
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool PeliculaExists(int id)
            {
                return _context.Peliculas.Any(e => e.Id == id);
            }
        }
    }

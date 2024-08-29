using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly WebApplication1Context _context;
        private readonly ILogger<MoviesController> _logger;
        public MoviesController(WebApplication1Context context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity is null");
            }
            IQueryable<string> genre = from m in _context.Movie orderby m.Genre select m.Genre;
            IQueryable<Movie> movies = from m in _context.Movie select m;
            if (!String.IsNullOrEmpty(search))
            {
                movies = movies.Where(s => s.Title!.ToUpper().Contains(search.ToUpper()));
            }
            GenreModel genreModel = new GenreModel()
            {
                Genres = new SelectList(await genre.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };
            return View(genreModel);

        }
            [HttpGet]
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var movie = await _context.Movie.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return View(movie);
            }
        [HttpGet]
            public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateData(int id)
        {
            return  RedirectToAction(nameof(Index));
        }
            public async Task<IActionResult> Details(int? id)
            {
            if (id==null)
            {
                return NotFound();
            }
            Movie? movie = await _context.Movie.FirstOrDefaultAsync(m=>m.Id == id);
            if (movie==null)
            {
                return NotFound();
            }
            return View(movie);
        }
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();  
            }
            var movie = await _context.Movie.FirstOrDefaultAsync(m=> m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
            [HttpPost,ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
            {
                if (id != movie.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(movie);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (id != movie.Id)
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
                return View(movie);
            }
        }
    }

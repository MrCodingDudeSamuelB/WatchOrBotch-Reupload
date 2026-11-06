
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchOrBotch.Models;
using WatchOrBotch.Service;

[Authorize]
public class MoviesController : Controller
{
    private readonly WatchOrBotchContext _context;

    private readonly AzureStorageService _azureStorage;
    public MoviesController(WatchOrBotchContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()
    {
        var sortedMovies = _context.Movie.OrderByDescending(m => m.Releasedate);

        return View(await sortedMovies.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [Authorize(Roles = "Admin")]
    // GET: MOVIES/Create
    public IActionResult Create()
    {
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");

        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,Runtime,AudienceRatingId,GenreId,Releasedate")] Movie movie, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            //Upload file to Azure Storage (after input validation occours)
            string imageUrl = await _azureStorage.UploadFileAsync(imageFile, imageFile.FileName);

            movie.ImageUrl = imageUrl;

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");

        return View(movie);
    }

    [Authorize(Roles = "Admin")]
    // GET: MOVIES/Edit/5
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
        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");

        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Synopsis,Runtime,AudienceRatingId,GenreId,Releasedate")] Movie movie)
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
                if (!MovieExists(movie.Id))
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

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var audiencerating = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(audiencerating, "Id", "Title");
            
        return View(movie);
    }

    [Authorize(Roles = "Admin")]
    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}

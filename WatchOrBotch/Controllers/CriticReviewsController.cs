using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchOrBotch.Models;
using WatchOrBotch;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class CriticReviewsController : Controller
{
    private readonly WatchOrBotchContext _context;

    private string? _userId;


    public CriticReviewsController(WatchOrBotchContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        _userId = User.GetUserId();
    }


    // GET: CRITICREVIEWS
    public async Task<IActionResult> Index()    
    {
        var reviews = await _context.CriticReview
        .Where(review => review.CreatedBy == _userId)
        .Include(review => review.Movie)
        .ToListAsync();

        return View(reviews);
    }

    // GET: CRITICREVIEWS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .Where(m => m.CreatedBy == _userId)
            .Include(h => h.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Create
    public IActionResult Create(int? movieId)
    {
        ViewData["MovieId"] = new SelectList(
            _context.Movie.OrderBy(movie => movie.Title),
            "Id",
            "Title",
            movieId);

        return View();
    }

    // POST: CRITICREVIEWS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,MovieId,Description,Rating,IsPublished,CreatedBy,CreatedDate")] CriticReview criticreview)
    {
        criticreview.CreatedBy = _userId;

        var reviewAlreadyExists = await _context.CriticReview.AnyAsync(review =>
            review.CreatedBy == _userId &&
            review.MovieId == criticreview.MovieId);

        if (reviewAlreadyExists)
        {
            ModelState.AddModelError(
                "MovieId",
                "You have already written a review for this movie.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(criticreview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["MovieId"] = new SelectList(
            _context.Movie.OrderBy(movie => movie.Title),
            "Id",
            "Title",
            criticreview.MovieId);

        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .Include(review => review.Movie)
            .Where(review => review.CreatedBy == _userId && !review.IsPublished)
            .FirstOrDefaultAsync(review => review.Id == id);

        if (criticreview == null)
        {
            return NotFound();
        }
        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,MovieId,Description,Rating,IsPublished,CreatedBy,CreatedDate")] CriticReview criticreview)
    {
        if (id != criticreview.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(criticreview);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriticReviewExists(criticreview.Id))
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

        ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title", criticreview.MovieId);

        return View(criticreview);
    }

    // GET: CRITICREVIEWS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var criticreview = await _context.CriticReview
            .Where(m => m.CreatedBy == _userId)
            .Include(h => h.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (criticreview == null)
        {
            return NotFound();
        }

        return View(criticreview);
    }

    // POST: CRITICREVIEWS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var criticreview = await _context.CriticReview.FindAsync(id);
        if (criticreview != null)
        {
            _context.CriticReview.Remove(criticreview);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CriticReviewExists(int? id)
    {
        return _context.CriticReview.Any(e => e.Id == id);
    }
}

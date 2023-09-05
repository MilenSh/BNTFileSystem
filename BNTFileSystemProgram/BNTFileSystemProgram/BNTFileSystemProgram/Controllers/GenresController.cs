using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using DataLayer;

namespace BNTFileSystemProgram.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDb<Genre, string> context;

        public GenresController(ApplicationDbContext applicationDbContext, IDb<Genre, string> context)
        {
            this.applicationDbContext = applicationDbContext;
            this.context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAllAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || applicationDbContext.Genres == null)
            {
                return NotFound();
            }

            var genre = await context.ReadAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Content")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await context.CreateAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || applicationDbContext.Genres == null)
            {
                return NotFound();
            }

            Genre genre = await context.ReadAsync(id);
            if (genre == null) { return NotFound(); }
            return View(genre);

            //await context.UpdateAsync(id);

            return View();
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GenreId,Content")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.UpdateAsync(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
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
            return View();
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || applicationDbContext.Genres == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);

            return View();
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (applicationDbContext.Genres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genres'  is null.");
            }
            await context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(string id)
        {
          return (applicationDbContext.Genres?.Any(e => e.GenreId == id)).GetValueOrDefault();
        }
    }
}

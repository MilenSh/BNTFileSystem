using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using DataLayer;
using ServiceLayer;

namespace BNTFileSystemProgram.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenreManager genreManager;

        public GenresController(GenreManager genreManager)
        {
            this.genreManager = genreManager;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await genreManager.ReadAllAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await genreManager.ReadAsync(id);

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
                await genreManager.CreateAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre genre = await genreManager.ReadAsync(id);
            if (genre == null) { return NotFound(); }
            return View(genre);
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
                    await genreManager.UpdateAsync(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenreExists(genre.GenreId))
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
            if (id == null)
            {
                return NotFound();
            }

            Genre genreFromDb = await genreManager.ReadAsync(id, false, true);

            if (genreFromDb == null)
            {
                return NotFound();
            }

            return View(genreFromDb);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await genreManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExists(string id)
        {
            return await genreManager.ReadAsync(id) is not null;
        }
    }
}

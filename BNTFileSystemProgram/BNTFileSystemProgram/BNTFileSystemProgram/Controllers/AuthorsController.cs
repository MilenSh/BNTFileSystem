using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using ServiceLayer;

namespace BNTFileSystemProgram.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorManager authorManager;

        public AuthorsController(AuthorManager authorManager)
        {
            this.authorManager = authorManager;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await authorManager.ReadAllAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await authorManager.ReadAsync(id, true, true);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,AuthorName")] Author author)
        {
            if (ModelState.IsValid)
            {
                await authorManager.CreateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //await context.UpdateAsync(id);
            Author author = await authorManager.ReadAsync(id);

            if(author == null) { return NotFound(); }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AuthorId,AuthorName")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await authorManager.UpdateAsync(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await AuthorExists(author.AuthorId))
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

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await authorManager.ReadAsync(id, false, false);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await authorManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AuthorExists(string id)
        {
            return await authorManager.ReadAsync(id) is not null;
        }
    }
}

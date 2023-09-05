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
    public class AuthorsController : Controller
    {
        private readonly IDb<Author, string> context;
        private readonly ApplicationDbContext applicationDbContext;

        public AuthorsController(ApplicationDbContext applicationDbContext, IDb<Author, string>  context)
        {
            this.applicationDbContext = applicationDbContext;
            this.context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAllAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || applicationDbContext.Authors == null)
            {
                return NotFound();
            }

            var author = await context.ReadAsync(id);

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
                await context.CreateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || applicationDbContext.Authors == null)
            {
                return NotFound();
            }

            //await context.UpdateAsync(id);


            Author author = await context.ReadAsync(id);
            if(author == null) { return NotFound(); }
            return View(author);

            return View();
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
                    await context.UpdateAsync(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            if (id == null || applicationDbContext.Authors == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);

            return View();
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (applicationDbContext.Authors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Authors'  is null.");
            }
            context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(string id)
        {
          return (applicationDbContext.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}

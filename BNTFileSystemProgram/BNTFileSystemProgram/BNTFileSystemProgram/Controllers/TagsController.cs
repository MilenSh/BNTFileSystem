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
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly TagContext context;

        public TagsController(ApplicationDbContext applicationDbContext, TagContext context)
        {
            this.context = context;
            this.applicationDbContext = applicationDbContext;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAllAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null ||applicationDbContext.Tags == null)
            {
                return NotFound();
            }

            var tag = await context.ReadAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,Content")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await context.CreateAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || applicationDbContext.Tags == null)
            {
                return NotFound();
            }

            await context.UpdateAsync(id);

            return View();
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TagId,Content")] Tag tag)
        {
            if (id != tag.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.UpdateAsync(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagId))
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

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || applicationDbContext.Tags == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);

            return View();
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (applicationDbContext.Tags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tags'  is null.");
            }

            await context.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(string id)
        {
          return (applicationDbContext.Tags?.Any(e => e.TagId == id)).GetValueOrDefault();
        }
    }
}

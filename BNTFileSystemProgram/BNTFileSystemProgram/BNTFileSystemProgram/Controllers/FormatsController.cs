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
    public class FormatsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IDb<Format, string> context;

        public FormatsController(ApplicationDbContext applicationDbContext, IDb<Format, string> context)
        {
            this.applicationDbContext = applicationDbContext;
            this.context = context;
        }

        // GET: Formats
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAllAsync());
        }

        // GET: Formats/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || applicationDbContext.Formats == null)
            {
                return NotFound();
            }

            var format = await context.ReadAsync(id);
            if (format == null)
            {
                return NotFound();
            }

            return View(format);
        }

        // GET: Formats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormatId,Extension")] Format format)
        {
            if (ModelState.IsValid)
            {
                await context.CreateAsync(format);
                return RedirectToAction(nameof(Index));
            }
            return View(format);
        }

        // GET: Formats/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || applicationDbContext.Formats == null)
            {
                return NotFound();
            }

            //await context.UpdateAsync(id);

            Format format = await context.ReadAsync(id);
            if (format == null) { return NotFound(); }
            return View(format);

            return View();
        }

        // POST: Formats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FormatId,Extension")] Format format)
        {
            if (id != format.FormatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.UpdateAsync(format);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormatExists(format.FormatId))
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

        // GET: Formats/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || applicationDbContext.Formats == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);

            return View();
        }

        // POST: Formats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (applicationDbContext.Formats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Formats'  is null.");
            }
            await context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FormatExists(string id)
        {
          return (applicationDbContext.Formats?.Any(e => e.FormatId == id)).GetValueOrDefault();
        }
    }
}

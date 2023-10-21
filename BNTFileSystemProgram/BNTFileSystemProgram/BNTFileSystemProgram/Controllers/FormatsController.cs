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
    public class FormatsController : Controller
    {
        private readonly FormatManager formatManager;

        public FormatsController(FormatManager formatManager)
        {
            this.formatManager = formatManager;
        }

        // GET: Formats
        public async Task<IActionResult> Index()
        {
            return View(await formatManager.ReadAllAsync());
        }

        // GET: Formats/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var format = await formatManager.ReadAsync(id);
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
                await formatManager.CreateAsync(format);
                return RedirectToAction(nameof(Index));
            }
            return View(format);
        }

        // GET: Formats/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Format format = await formatManager.ReadAsync(id);
            if (format == null) { return NotFound(); }
            return View(format);
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
                    await formatManager.UpdateAsync(format);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FormatExists(format.FormatId))
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
            if (id == null)
            {
                return NotFound();
            }

            Format formatFromDb = await formatManager.ReadAsync(id, false, true);

            if(formatFromDb == null)
            {
                return NotFound();
            }

            return View(formatFromDb);
        }

        // POST: Formats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await formatManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FormatExists(string id)
        {
          return await formatManager.ReadAsync(id) is not null;
        }
    }
}

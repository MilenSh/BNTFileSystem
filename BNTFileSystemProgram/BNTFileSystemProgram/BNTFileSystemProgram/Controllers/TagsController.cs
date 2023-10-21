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
    public class TagsController : Controller
    {
        private readonly TagManager tagManager;

        public TagsController(TagManager tagManager)
        {
            this.tagManager = tagManager;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await tagManager.ReadAllAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await tagManager.ReadAsync(id);

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
                await tagManager.CreateAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag tag = await tagManager.ReadAsync(id);
            if (tag == null) { return NotFound(); }
            return View(tag);
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
                    await tagManager.UpdateAsync(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TagExists(tag.TagId))
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
            if (id == null)
            {
                return NotFound();
            }

            Tag tagFromDb = await tagManager.ReadAsync(id, false, true);

            if (tagFromDb == null)
            {
                return NotFound();
            }

            return View(tagFromDb);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await tagManager.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TagExists(string id)
        {
          return await tagManager.ReadAsync(id) is not null;
        }
    }
}

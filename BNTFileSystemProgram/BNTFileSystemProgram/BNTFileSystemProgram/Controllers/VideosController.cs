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
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly VideoContext context;

        public VideosController(ApplicationDbContext applicationDbContext, VideoContext context)
        {
            this.context = context;
            this.applicationDbContext = applicationDbContext;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAllAsync());
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || applicationDbContext.Videos == null)
            {
                return NotFound();
            }

            var video = await context.ReadAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            //ViewData["FormatId"] = new SelectList(_context.Formats, "FormatId", "FormatId");
            //^^^ Тук нямам представа какво трябва да е ^^^
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,Title,Location,FormatId,Size,Description,Comment,Year,Copyright")] Video video)
        {
            if (ModelState.IsValid)
            {
                await context.CreateAsync(video);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["FormatId"] = new SelectList(_context.Formats, "FormatId", "FormatId", video.FormatId);
            //^^^ Тук също ^^^
            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || applicationDbContext.Videos == null)
            {
                return NotFound();
            }

            await context.UpdateAsync(id);

            //ViewData["FormatId"] = new SelectList(_context.Formats, "FormatId", "FormatId", video.FormatId);
            //^^^ И тук ^^^
            return View();
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VideoId,Title,Location,FormatId,Size,Description,Comment,Year,Copyright")] Video video)
        {
            if (id != video.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.UpdateAsync(video);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.VideoId))
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
            //ViewData["FormatId"] = new SelectList(_context.Formats, "FormatId", "FormatId", video.FormatId);
            //^^^ И тук, да ^^^
            return View();
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || applicationDbContext.Videos == null)
            {
                return NotFound();
            }

            await context.DeleteAsync(id);

            return View();
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (applicationDbContext.Videos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Videos'  is null.");
            }
            await context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(string id)
        {
          return (applicationDbContext.Videos?.Any(e => e.VideoId == id)).GetValueOrDefault();
        }
    }
}

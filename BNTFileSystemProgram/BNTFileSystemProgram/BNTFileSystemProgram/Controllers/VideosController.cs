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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BNTFileSystemProgram.Controllers
{
    public class VideosController : Controller
    {
        private readonly VideoManager videoManager;

        public VideosController(VideoManager videoManager)
        {
            this.videoManager = videoManager;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            return View(await videoManager.ReadAllAsync(true));
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await videoManager.ReadAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            ViewData["Format"] = new SelectList(ContextHelper.GetDbContext().Formats, "FormatId", "Extension");
            ViewData["Genres"] = ContextHelper.GetDbContext().Genres.ToList();
            ViewData["Authors"] = ContextHelper.GetDbContext().Authors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,Title,Location,FormatId,Size,Description,Comment,Year,Copyright")] Video video)
        {
            //Figure out how to recieve chackbox data
            if (ModelState.IsValid)
            {
                await videoManager.CreateAsync(video);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Format"] = ContextHelper.GetDbContext().Formats.ToList();
            ViewData["Format"] = new SelectList(ContextHelper.GetDbContext().Formats, "FormatId", "Extension", video.FormatId);
            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Video video = await videoManager.ReadAsync(id, true);
            if (video == null) { return NotFound(); }
            ViewData["Format"] = new SelectList(ContextHelper.GetDbContext().Formats, "FormatId", "Extension", video.FormatId);
            return View(video);
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
                    await videoManager.UpdateAsync(video);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VideoExists(video.VideoId))
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

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Video videoFromDb = await videoManager.ReadAsync(id, false, true);

            if (videoFromDb == null)
            {
                return NotFound();
            }

            return View(videoFromDb);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await videoManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> VideoExists(string id)
        {
          return await videoManager.ReadAsync(id) is not null;
        }
    }
}

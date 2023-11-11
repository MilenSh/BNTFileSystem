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
        private readonly AuthorManager authorManager;
        private readonly GenreManager genreManager;
        private readonly TagManager tagManager;

        public VideosController(VideoManager videoManager, AuthorManager authorManager, GenreManager genreManager, TagManager tagManager)
        {
            this.videoManager = videoManager;
            this.authorManager = authorManager;
            this.genreManager = genreManager;
            this.tagManager = tagManager;
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

            var video = await videoManager.ReadAsync(id, true);

            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            LoadNavigationalProperties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            Video video = await SetVideoPropertiesAsync(formCollection);
            if (ModelState.IsValid)
            {
                await videoManager.CreateAsync(video);
                return RedirectToAction(nameof(Index));
            }
            LoadNavigationalProperties();
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
            LoadNavigationalProperties();
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IFormCollection formCollection)
        {
            if (id != formCollection["VideoId"])
            {
                return NotFound();
            }

            Video video = await SetVideoPropertiesAsync(formCollection);
            video.VideoId = id;

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

        public void LoadNavigationalProperties()
        {
            ViewData["Format"] = new SelectList(ContextHelper.GetDbContext().Formats, "FormatId", "Extension") ;
            ViewData["Genres"] = ContextHelper.GetDbContext().Genres.ToList();
            ViewData["Authors"] = ContextHelper.GetDbContext().Authors.ToList();
            ViewData["Tags"] = ContextHelper.GetDbContext().Tags.ToList();
        }

        public async Task<Video> SetVideoPropertiesAsync(IFormCollection formCollection)
        {
            Author authorFromDb;
            Genre genreFromDb;
            Tag tagFromDb;
            Video video = new();

            foreach (string item in formCollection["Authors"])
            {
                authorFromDb = await authorManager.ReadAsync(item);
                if (authorFromDb != null)
                {
                    video.Authors.Add(authorFromDb);
                }
            }

            foreach (string item in formCollection["Genres"])
            {
                genreFromDb = await genreManager.ReadAsync(item);
                if (genreFromDb != null)
                {
                    video.Genres.Add(genreFromDb);
                }
            }

            foreach (string item in formCollection["Tags"])
            {
                tagFromDb = await tagManager.ReadAsync(item);
                if (tagFromDb != null)
                {
                    video.Tags.Add(tagFromDb);
                }
            }

            video.Title = formCollection["Title"];
            video.Description = formCollection["Description"];
            video.FormatId = formCollection["FormatId"];
            video.Location = formCollection["Location"];
            video.Size = double.Parse(formCollection["Size"]);
            video.Comment = formCollection["Comment"];
            video.Copyright = formCollection["Copyright"];
            video.Year = int.Parse(formCollection["Year"]);

            return video;
        }
    }
}

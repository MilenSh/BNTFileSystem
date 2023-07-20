using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VideoContext : IDb<Video, string>
    {
        private readonly ApplicationDbContext _context;

        public VideoContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Video item)
        {
            try
            {
                _context.Videos.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Video> ReadAsync(string key)
        {
            try
            {
                return await _context.Videos.
                    Include(a => a.Authors).
                    Include(f => f.Format).
                    Include(g => g.Genres).
                    Include(t => t.Tags).
                    SingleAsync(v => v.VideoId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Video>> ReadAllAsync()
        {
            try
            {
                return await _context.Videos.
                    Include(a => a.Authors).
                    Include(f => f.Format).
                    Include(g => g.Genres).
                    Include(t => t.Tags).
                    ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Video item)
        {
            try
            {
                _context.Videos.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateAsync(string key)
        {
            try
            {
                Video? video = await _context.Videos.FindAsync(key);
                if(video != null)
                {
                    _context.Videos.Update(video);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                var videoFromDb = await _context.Videos.FindAsync(key);
                if(videoFromDb != null)
                {
                    _context.Videos.Remove(videoFromDb);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

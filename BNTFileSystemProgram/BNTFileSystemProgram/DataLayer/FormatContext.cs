using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FormatContext : IDb<Format, string>
    {
        private readonly ApplicationDbContext dbContext;

        public FormatContext(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync(Format item)
        {
            try
            {
                dbContext.Formats.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Format> ReadAsync(string key, bool useNavigationalOptions = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Format> query = dbContext.Formats;

                if (useNavigationalOptions)
                {
                    query.Include(f => f.Videos);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(f => f.FormatId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Format>> ReadAllAsync(bool useNavigationalOptions = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Format> query = dbContext.Formats;

                if (useNavigationalOptions)
                {
                    query.Include(f => f.Videos);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Format item, bool useNavigationalOptions = false)
        {
            try
            {
                Format formatFromDb = await ReadAsync(item.FormatId, useNavigationalOptions, false);

                if(formatFromDb == null) { await CreateAsync(item); }

                formatFromDb.Extension = item.Extension;

                if (useNavigationalOptions)
                {
                    List<Video> videos = new List<Video>(item.Videos.Count);

                    foreach(Video video in item.Videos)
                    {
                        Video videoFromDb = await dbContext.Videos.FindAsync(video.VideoId);

                        if(videoFromDb != null)
                        {
                            videos.Add(videoFromDb);
                        } 
                        else
                        {
                            videos.Add(video);
                        }
                    }

                    formatFromDb.Videos = videos;

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
                var formatFromDb = await ReadAsync(key, false, false);

                if(formatFromDb == null)
                {
                    throw new ArgumentException("The format with this Id does not exist");
                }

                dbContext.Formats.Remove(formatFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

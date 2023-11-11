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
    public class GenreContext : IDb<Genre, string>
    {
        private readonly ApplicationDbContext dbContext;

        public GenreContext(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync(Genre item)
        {
            try
            {
                dbContext.Genres.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Videos);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(g => g.GenreId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Genre>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Genre> query = dbContext.Genres;

                if (useNavigationalProperties)
                {
                    query = query.Include(g => g.Videos);
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

        public async Task UpdateAsync(Genre item, bool useNavigationalProperties = false)
        {
            try
            {
                Genre genreFromDb = await ReadAsync(item.GenreId, useNavigationalProperties, false);

                genreFromDb.Content = item.Content;

                if (useNavigationalProperties)
                {
                    List<Video> videos = new List<Video>();

                    foreach (Video video in item.Videos)
                    {
                        Video videoFromDb = await dbContext.Videos.FindAsync(video.VideoId);

                        if (videoFromDb != null)
                        {
                            videos.Add(videoFromDb);
                        }
                        else
                        {
                            videos.Add(video);
                        }
                    }

                    genreFromDb.Videos = videos;
                }

                await dbContext.SaveChangesAsync();
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
                Genre genreFromDb = await ReadAsync(key, false, false);

                if (genreFromDb == null)
                {
                    throw new ArgumentException("The genre with this Id does not exist");
                }

                dbContext.Genres.Remove(genreFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

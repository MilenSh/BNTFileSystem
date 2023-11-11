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
    public class TagContext : IDb<Tag, string>
    {
        private readonly ApplicationDbContext dbContext;

        public TagContext(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync(Tag item)
        {
            try
            {
                dbContext.Tags.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Tag> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Tag> query = dbContext.Tags;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Videos);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(t => t.TagId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Tag>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Tag> query = dbContext.Tags;

                if (useNavigationalProperties)
                {
                    query = query.Include(t => t.Videos);
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

        public async Task UpdateAsync(Tag item, bool useNavigationalProperties = false)
        {
            try
            {
                Tag tagFromDb = await ReadAsync(item.TagId, useNavigationalProperties, false);

                tagFromDb.Content = item.Content;

                if (useNavigationalProperties)
                {
                    List<Video> videos = new();

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

                    tagFromDb.Videos = videos;
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
                var tagFromDb = await ReadAsync(key, false, false);

                if (tagFromDb == null)
                {
                    throw new ArgumentException("The tag with this Id does not exist");
                }

                dbContext.Tags.Remove(tagFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

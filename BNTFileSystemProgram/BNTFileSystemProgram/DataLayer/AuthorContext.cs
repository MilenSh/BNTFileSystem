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
    public class AuthorContext : IDb<Author, string>
    {
        private readonly ApplicationDbContext dbContext;

        public AuthorContext(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateAsync(Author item)
        {
            try
            {
                dbContext.Authors.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {

            try
            {
                IQueryable<Author> query = dbContext.Authors;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Videos);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => a.AuthorId == key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Author>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Author> query = dbContext.Authors;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Videos);
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

        public async Task UpdateAsync(Author item, bool useNavigationalProperties = false)

        {
            try
            {
                Author authorFromDb = await ReadAsync(item.AuthorId, useNavigationalProperties, false);

                if (authorFromDb == null) { await CreateAsync(item); }

                authorFromDb.AuthorName = item.AuthorName;

                if (useNavigationalProperties)
                {
                    List<Video> videos = new(item.Videos.Count);

                    foreach (var video in item.Videos)
                    {
                        Video videoFromDb = await dbContext.Videos.FindAsync(video.VideoId);

                        if (videoFromDb is null)
                        {
                            videos.Add(video);
                        }
                        else
                        {
                            videos.Add(videoFromDb);
                        }
                    }

                    authorFromDb.Videos = videos;
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
                Author authorFromDb = await ReadAsync(key, false, false);

                if (authorFromDb == null)
                {
                    throw new ArgumentException("Author with this Id does not exist");
                }

                dbContext.Authors.Remove(authorFromDb);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TagContext : IDb<Tag, string>
    {
        private readonly ApplicationDbContext _context;

        public TagContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Tag item)
        {
            try
            {
                _context.Tags.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Tag> ReadAsync(string key)
        {
            try
            {
                return await _context.Tags.
                    Include(v => v.Videos).
                    SingleAsync(t => t.TagId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Tag>> ReadAllAsync()
        {
            try
            {
                return await _context.Tags.
                    Include(v => v.Videos).
                    ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Tag item)
        {
            try
            {
                _context.Tags.Update(item);
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
                Tag? tag = await _context.Tags.FindAsync(key);
                if(tag != null)
                {
                    _context.Tags.Update(tag);
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
                var tagFromDb = await _context.Tags.FindAsync(key);
                if (tagFromDb != null)
                {
                    _context.Tags.Remove(tagFromDb);
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

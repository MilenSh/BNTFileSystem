using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FormatContext : IDb<Format, string>
    {
        private readonly ApplicationDbContext _context;

        public FormatContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Format item)
        {
            try
            {
                _context.Formats.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Format> ReadAsync(string key)
        {
            try
            {
                return await _context.Formats.
                    Include(v => v.Videos).
                    SingleAsync(f => f.FormatId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Format>> ReadAllAsync()
        {
            try
            {
                return await _context.Formats.
                    Include(v => v.Videos).
                    ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Format item)
        {
            try
            {
                _context.Formats.Update(item);
                await _context.SaveChangesAsync();
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
                var formatFromDb = await _context.Formats.FindAsync(key);
                _context.Formats.Remove(formatFromDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

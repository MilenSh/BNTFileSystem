using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GenreContext : IDb<Genre, string>
    {
        private readonly ApplicationDbContext _context;

        public GenreContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Genre item)
        {
            try
            {
                _context.Genres.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Genre> ReadAsync(string key)
        {
            try
            {
                return await _context.Genres.
                    Include(v => v.Videos).
                    SingleAsync(g => g.GenreId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Genre>> ReadAllAsync()
        {
            try
            {
                return await _context.Genres.
                    Include(v => v.Videos).
                    ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Genre item)
        {
            try
            {
                _context.Genres.Update(item);
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
                var genreFromDb = await _context.Genres.FindAsync(key);
                _context.Genres.Remove(genreFromDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

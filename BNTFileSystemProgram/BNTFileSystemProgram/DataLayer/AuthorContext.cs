using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AuthorContext : IDb<Author, string>
    {
        private readonly ApplicationDbContext _context;

        public AuthorContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Author item)
        {
            try
            {
                _context.Authors.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> ReadAsync(string key)
        {
            try
            {
                return await _context.Authors.
                    Include(v => v.Videos).
                    SingleAsync(a => a.AuthorId == key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Author>> ReadAllAsync()
        {
            try
            {
                return await _context.Authors.
                    Include(v => v.Videos).
                    ToListAsync();
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
                Author? author = await _context.Authors.FindAsync(key);
                if (author != null)
                {
                    _context.Authors.Update(author);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateAsync(Author item)
        {
            try
            {
                    _context.Authors.Update(item);
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
                var authorFromDb = await _context.Authors.FindAsync(key);
                if (authorFromDb != null)
                {
                    _context.Authors.Remove(authorFromDb);
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

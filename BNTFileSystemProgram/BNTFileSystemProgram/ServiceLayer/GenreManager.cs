using BussinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class GenreManager
    {
        private readonly GenreContext genreContext;

        public GenreManager(GenreContext formatContext)
        {
            this.genreContext = formatContext;
        }

        public async Task CreateAsync(Genre item)
        {
            // Validate data ... other logic eventually
            await genreContext.CreateAsync(item);
        }

        public async Task<Genre> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await genreContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Genre>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await genreContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Genre item, bool useNavigationalProperties = false)
        {
            await genreContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await genreContext.DeleteAsync(key);
        }
    }
}

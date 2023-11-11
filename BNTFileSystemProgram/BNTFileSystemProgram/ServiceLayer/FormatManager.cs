using BussinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class FormatManager
    {
        private readonly FormatContext formatContext;

        public FormatManager(FormatContext formatContext)
        {
            this.formatContext = formatContext;
        }

        public async Task CreateAsync(Format item)
        {
            // Validate data ... other logic eventually
            await formatContext.CreateAsync(item);
        }

        public async Task<Format> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await formatContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Format>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await formatContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Format item, bool useNavigationalProperties = false)
        {
            await formatContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await formatContext.DeleteAsync(key);
        }
    }
}

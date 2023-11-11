using BussinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TagManager
    {
        private readonly TagContext tagContext;

        public TagManager(TagContext tagContext)
        {
            this.tagContext = tagContext;
        }

        public async Task CreateAsync(Tag item)
        {
            // Validate data ... other logic eventually
            await tagContext.CreateAsync(item);
        }

        public async Task<Tag> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await tagContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Tag>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await tagContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Tag item, bool useNavigationalProperties = false)
        {
            await tagContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await tagContext.DeleteAsync(key);
        }
    }
}

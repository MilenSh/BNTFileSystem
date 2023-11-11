using BussinessLayer;
using DataLayer;

namespace ServiceLayer
{
    public class AuthorManager
    {
        private readonly AuthorContext authorContext;

        public AuthorManager(AuthorContext authorContext)
        {
            this.authorContext = authorContext;
        }

        public async Task CreateAsync(Author author)
        {
            // Validate data ... other logic eventually
            await authorContext.CreateAsync(author);
        }

        public async Task<Author> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await authorContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Author>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await authorContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Author author, bool useNavigationalProperties = false)
        {
            await authorContext.UpdateAsync(author, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await authorContext.DeleteAsync(key);
        }
    }
}
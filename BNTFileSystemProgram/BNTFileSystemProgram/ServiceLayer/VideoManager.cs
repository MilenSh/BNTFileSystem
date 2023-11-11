using BussinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class VideoManager
    {
        private readonly VideoContext videoContext;

        public VideoManager(VideoContext videoContext)
        {
            this.videoContext = videoContext;
        }

        public async Task CreateAsync(Video item)
        {
            // Validate data ... other logic eventually
            await videoContext.CreateAsync(item);
        }

        public async Task<Video> ReadAsync(string key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await videoContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<Video>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await videoContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Video item, bool useNavigationalProperties = false)
        {
            await videoContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(string key)
        {
            await videoContext.DeleteAsync(key);
        }
    }
}

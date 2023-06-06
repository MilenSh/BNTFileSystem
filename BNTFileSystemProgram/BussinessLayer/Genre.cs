using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Genre
    {
        private Genre()
        {
            Videos = new();
        }
        public Genre(int genreId, string content) : this()
        {
            GenreId = genreId;
            Content = content;
        }

        public int GenreId { get; set; }
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Genre
    {
        [Key]
        public string GenreId { get; set; }
        [Required]
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
        public Genre()
        {
            GenreId = Guid.NewGuid().ToString();
            Videos = new();
        }
        public Genre(string genreId, string content) : this()
        {
            GenreId = genreId;
            Content = content;
        }
    }
}

using BussinessLayer;
using System.ComponentModel.DataAnnotations;

namespace BNTFileSystemProgram.Models
{
    public class GenreViewModel
    {
        public string GenreId { get; set; }
        [Required]
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
    }
}

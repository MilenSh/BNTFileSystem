using BussinessLayer;
using System.ComponentModel.DataAnnotations;

namespace BNTFileSystemProgram.Models
{
    public class AuthorViewModel
    {
        public string AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public List<Video> Videos { get; set; }
    }
}

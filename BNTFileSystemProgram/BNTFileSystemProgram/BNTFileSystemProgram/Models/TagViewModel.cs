using BussinessLayer;
using System.ComponentModel.DataAnnotations;

namespace BNTFileSystemProgram.Models
{
    public class TagViewModel
    {
        public string TagId { get; set; }
        [Required]
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
    }
}

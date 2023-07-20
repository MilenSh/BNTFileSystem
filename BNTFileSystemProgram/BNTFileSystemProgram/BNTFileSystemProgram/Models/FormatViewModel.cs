using BussinessLayer;
using System.ComponentModel.DataAnnotations;

namespace BNTFileSystemProgram.Models
{
    public class FormatViewModel
    {
        public string FormatId { get; set; }
        [Required]
        public string Extension { get; set; }
        public List<Video> Videos { get; set; }
    }
}

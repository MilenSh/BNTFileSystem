using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Format
    {
        [Key]
        public string FormatId { get; set; }
        [Required]
        public string Extension { get; set; }
        public List<Video> Videos { get; set; }
        public Format()
        {
            FormatId = Guid.NewGuid().ToString();
            Videos = new();
        }
        public Format(string formatId, string extension) : this()
        {
            FormatId = formatId;
            Extension = extension;
        }
    }
}

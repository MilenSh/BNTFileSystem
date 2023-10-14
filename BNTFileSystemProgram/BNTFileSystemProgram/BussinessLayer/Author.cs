using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Author
    {
        [Key]
        public string AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }
        public List<Video> Videos { get; set; }
        public Author()
        {
            AuthorId = Guid.NewGuid().ToString();
            Videos = new();
        }
        
        public Author(string authorId, string authorName) : this()
        {
            AuthorId = authorId;
            AuthorName = authorName;
        }
    }
}

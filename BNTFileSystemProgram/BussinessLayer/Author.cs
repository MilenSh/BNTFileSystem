using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Author
    {
        private Author()
        {
            Videos = new();
        }
        public Author(int authorId, string authorName) : this()
        {
            AuthorId = authorId;
            AuthorName = authorName;
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<Video> Videos { get; set; }
    }
}

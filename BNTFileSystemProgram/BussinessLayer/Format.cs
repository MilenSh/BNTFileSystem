using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class Format
    {
        private Format()
        {
            Videos = new();
        }
        public Format(int formatId, string extension) : this()
        {
            FormatId = formatId;
            Extension = extension;
        }

        public int FormatId { get; set; }
        public string Extension { get; set; }
        public List<Video> Videos { get; set; }
    }
}

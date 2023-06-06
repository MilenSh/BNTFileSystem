using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace BussinessLayer
{
    public class Genres
    {
        [Key]
        public string Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //TODO: add list of entries in videos

    }
}

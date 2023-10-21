using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BussinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace BNTFileSystemProgram.Models
{
    public class VideoViewModel
    {
        public string VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }

        [ForeignKey("FormatId")]
        public string FormatId { get; set; }
        public Format Format { get; set; }
        public List<Genre> Genres { get; set; }
        public double Size { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public string Comment { get; set; }
        public int Year { get; set; }
        public List<Author> Authors { get; set; }
        public string Copyright { get; set; }

        //Валидация?
    }
}

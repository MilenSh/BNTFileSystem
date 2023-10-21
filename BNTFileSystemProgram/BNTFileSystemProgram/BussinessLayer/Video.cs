

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer
{
    public class Video
    {
        [Key]
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
        public Video()
        {
            VideoId = Guid.NewGuid().ToString();
            Authors = new List<Author>();
            Tags = new List<Tag>();
            Genres = new List<Genre>();
        }
        public Video(string videoId, string title, string location, Format format, double size, string description, string comment, int year, string copyright) : this()
        {
            VideoId = videoId;
            Title = title;
            Location = location;
            Format = format;
            Size = size;
            Description = description;
            Comment = comment;
            Year = year;
            Copyright = copyright;
        }
    }
}
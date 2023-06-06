

namespace BussinessLayer
{
    public class Video
    {
        private Video()
        {
            Authors = new List<Author>();
            Tags = new List<Tag>();
            Genres = new List<Genre>();
        }
        public Video(int videoId, string title, string location, Format format, double size, string description, string comment, int year, string copyright) : this()
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

        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public Format Format { get; set; }
        public List<Genre> Genres { get; set; }
        public double Size { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public string Comment { get; set; }
        public int Year { get; set; }
        public List<Author> Authors { get; set; }
        public string Copyright { get; set; }
    }
}
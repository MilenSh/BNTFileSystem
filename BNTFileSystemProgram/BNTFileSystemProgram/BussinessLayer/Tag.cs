using System.ComponentModel.DataAnnotations;

namespace BussinessLayer
{
    public class Tag
    {
        [Key]
        public string TagId { get; set; }

        [Required]
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
        public Tag()
        {
            TagId = Guid.NewGuid().ToString();
            Videos = new();
        }
        public Tag(string tagId, string content) : this()
        {
            TagId = tagId;
            Content = content;
        }
    }
}
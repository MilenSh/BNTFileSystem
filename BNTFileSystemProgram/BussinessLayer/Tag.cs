namespace BussinessLayer
{
    public class Tag
    {
        private Tag()
        {
            Videos = new();
        }
        public Tag(int tagId, string content) : this()
        {
            TagId = tagId;
            Content = content;
        }

        public int TagId { get; set; }
        public string Content { get; set; }
        public List<Video> Videos { get; set; }
    }
}
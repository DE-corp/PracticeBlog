namespace PracticeBlog.Data.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

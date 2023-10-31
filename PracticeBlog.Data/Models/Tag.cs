namespace PracticeBlog.Data.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}

namespace PracticeBlog.Data.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ArticleID { get; set; }
        public Article Article { get; set; }
    }
}

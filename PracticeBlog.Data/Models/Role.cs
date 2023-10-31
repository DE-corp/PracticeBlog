namespace PracticeBlog.Data.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

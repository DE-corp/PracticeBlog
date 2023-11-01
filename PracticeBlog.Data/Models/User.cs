namespace PracticeBlog.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Admin";
        public int Age { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}

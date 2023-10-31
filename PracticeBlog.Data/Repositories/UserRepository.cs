using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(BlogContext db) : base(db) { }

        public new async Task Add(User user)
        {
            user.Roles.Add(new Role { ID = 1, Name = "Admin" });
            Set.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}

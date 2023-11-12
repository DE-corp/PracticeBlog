using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(BlogContext db) : base(db) { }

    }
}

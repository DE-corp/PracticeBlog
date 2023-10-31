using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(BlogContext db) : base(db) { }

    }
}

using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(BlogContext db) : base(db) { }

    }
}

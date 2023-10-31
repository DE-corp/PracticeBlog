using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public class ArticlesRepository : Repository<Article>
    {
        public ArticlesRepository(BlogContext db) : base(db) { }

        public IEnumerable<Article> GetArticlesByAuthorId(int userId)
        {
            var articles = Set.AsEnumerable().Where(x => x.UserID == userId);

            return articles.ToList();
        }
    }
}

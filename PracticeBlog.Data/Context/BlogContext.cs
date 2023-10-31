using Microsoft.EntityFrameworkCore;
using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Context
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Article>().ToTable("Articles");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Comment>().ToTable("Comments");

            builder.Entity<Comment>()
                .HasOne(a => a.User)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.UserID)
                .HasPrincipalKey(d => d.ID)
                .IsRequired(false);
        }
    }
}

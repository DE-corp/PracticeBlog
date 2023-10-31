using Microsoft.EntityFrameworkCore;
using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;
using PracticeBlog.Extentions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Add DB
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services
            .AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString))
            .AddUnitOfWork()
                .AddCustomRepository<User, UserRepository>()
                .AddCustomRepository<Article, ArticlesRepository>()
                .AddCustomRepository<Comment, CommentRepository>()
                .AddCustomRepository<Tag, TagRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
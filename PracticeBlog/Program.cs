using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;
using PracticeBlog.Extentions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DB
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services
            .AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString))
            .AddUnitOfWork()
                .AddCustomRepository<User, UserRepository>()
                .AddCustomRepository<Article, ArticlesRepository>()
                .AddCustomRepository<Comment, CommentRepository>()
                .AddCustomRepository<Tag, TagRepository>();

        builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "PracticeBlog", Version = "v1" }); });

        builder.Services.AddAuthentication(options => options.DefaultScheme = "Coockies").AddCookie("Coockies", options =>
        {
            options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
            {
                OnRedirectToLogin = redirectContext =>
                {
                    redirectContext.HttpContext.Response.StatusCode = 401;
                    return Task.CompletedTask;
                }
            };
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeBlog v1"));

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;
using PracticeBlog.Extentions;
using PracticeBlog.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("Запуск приложения");

        try
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
                    .AddCustomRepository<Tag, TagRepository>()
                    .AddCustomRepository<Role, RoleRepository>(); ;

            builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "PracticeBlog", Version = "v1" }); });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.LoginPath = new PathString("/Authenticate");
                  });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Приложение остановлено из-за исключения");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }

    }
}
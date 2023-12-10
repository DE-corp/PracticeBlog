using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PracticeBlog.Api.Extentions;
using PracticeBlog.Api.Middlewares;
using PracticeBlog.Data.Context;
using PracticeBlog.Data.Models;
using PracticeBlog.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.LoginPath = new PathString("/Authenticate");
                  });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "PracticeBlog", Version = "v1" }); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeBlog v1"));
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

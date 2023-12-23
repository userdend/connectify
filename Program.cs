using Connectify.Data;
using Connectify.Interfaces;
using Connectify.Repositories;
using Connectify.ResponseModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
        option => {
            option.LoginPath = "/User/Login";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        }
    );

// Configure dependency injection to provide an instance of interface to the controller.
builder.Services.AddHttpClient<IUserRepository<UserResponseModel>, UserRepository>();
builder.Services.AddHttpClient<IPostsRepository<PostsResponseModel>, PostsRepository>();
builder.Services.AddHttpClient<ICommentRepository<CommentResponseModel>, CommentRepository>();

builder.Services.AddScoped<IUserRepository<UserResponseModel>, UserRepository>();
builder.Services.AddScoped<ICommentRepository<CommentResponseModel>, CommentRepository>();

// Add database context to the application.
builder.Services.AddDbContext<AppDbContext>(
        options => {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        }
    );

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Connectify.Models;
using Microsoft.EntityFrameworkCore;

namespace Connectify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<UserPasswordModel> UserPassword { get; set; }
        public DbSet<UserInfoModel> UserInfo { get; set; }
        public DbSet<CommentModel> Comment { get; set; }
    }
}

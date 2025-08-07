using Microsoft.EntityFrameworkCore;

namespace OREChinese.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Video> Videos { get; set; }

    }



}

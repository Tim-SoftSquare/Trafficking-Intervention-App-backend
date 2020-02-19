using Microsoft.EntityFrameworkCore;

namespace Trafficking_Intervention_backend {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
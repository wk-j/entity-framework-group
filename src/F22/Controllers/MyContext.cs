using Microsoft.EntityFrameworkCore;

namespace GroupBy.Controllers {
    public class AppContext : DbContext {
        public AppContext(DbContextOptions options) : base(options) {

        }

        public DbSet<Student> Students { set; get; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace GroupBy.Controllers
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }
    }
}
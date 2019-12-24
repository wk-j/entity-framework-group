using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace GroupBy.Controllers
{

    [Route("api/[controller]/[action]")]
    public class GroupController : ControllerBase
    {
        ILoggerFactory fact;
        public GroupController(ILoggerFactory factory)
        {
            this.fact = factory;
        }

        DbContextOptions Options()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseMySql("Host=localhost;User Id=root; Password=1234;Database=App")
                .UseLoggerFactory(fact)
                .Options;
            return options;
        }

        [HttpGet]
        public int Insert()
        {
            var options = Options();
            using var context = new MyContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Students.AddRange(new[] {
                new Student {
                    Course = "cs",
                    Score = 80
                },
                new Student {
                    Course = "cs",
                    Score = 90
                },
                new Student {
                    Course = "th",
                    Score = 20
                },
                new Student {
                    Course = "th",
                    Score = 80
                },
                new Student {
                    Course = "en",
                    Score = 80
                }
            });
            return context.SaveChanges();
        }

        [HttpGet]
        public IEnumerable<Student> Group()
        {
            var options = Options();
            using var context = new MyContext(options);
            var student = context.Students
                .GroupBy(x => x.Course)
                .Select(x => x.FirstOrDefault())
                .ToList();

            return student;
        }
    }
}
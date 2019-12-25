using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace GroupBy.Controllers {

    public partial class GroupController {
        [HttpGet]
        public IEnumerable<Student> All() {
            var options = Options();
            using var context = new AppContext(options);
            return context.Students.ToArray();
        }

        [HttpGet]
        public IEnumerable<dynamic> GroupKey() {
            var options = Options();
            using var context = new AppContext(options);
            var student = context.Students
                .GroupBy(x => x.Course)
                .Select(x => new {
                    Key = x.Key,
                    Count = x.Count()
                })
                .ToList();

            return student;
        }

        [HttpGet]
        public IEnumerable<Student> FirstOrDefault() {
            var options = Options();
            using var context = new AppContext(options);
            var student = context.Students
                .GroupBy(x => x.Course)
                .Select(x => x.First())
                .OrderBy(x => x.Score)
                .ToList();

            return student;
        }
    }

    [Route("api/[controller]/[action]")]
    public partial class GroupController : ControllerBase {
        ILoggerFactory fact;
        public GroupController() {
            var fact = new LoggerFactory().AddConsole();
            this.fact = fact;
        }

        DbContextOptions Options() {
            // DbContextOptions options = new DbContextOptionsBuilder()
            //     .UseMySql("Host=localhost;User Id=root; Password=1234;Database=App")
            //     .UseLoggerFactory(fact)
            //     .Options;

            DbContextOptions options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;User Id=root; Password=1234;Database=app")
                .UseLoggerFactory(fact)
                .Options;


            return options;
        }

        [HttpGet]
        public int Insert() {
            var options = Options();
            using var context = new AppContext(options);
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
    }
}
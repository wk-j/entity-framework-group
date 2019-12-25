using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace GroupBy.Controllers {
    [Route("api/[controller]/[action]")]

    public partial class GroupController {
        [HttpGet]
        public IEnumerable<dynamic> SubQuery() {
            var options = Options();
            using var context = new AppContext(options);

            var rs =
                from top in (
                    from student in context.Students
                    group student by student.Course into students
                    select new { students.Key, Top = students.Max(x => x.Score) }
                )
                join student in context.Students on top.Key equals student.Course
                where student.Score == top.Top
                select student;

            return rs.ToList();
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
                .OrderBy(x => x.Score);

            return student.ToList();
        }

        [HttpGet]
        public IEnumerable<Student> AsEnumerable() {
            var options = Options();
            using var context = new AppContext(options);
            var student = context.Students
                .AsEnumerable()
                .GroupBy(x => x.Course)
                .Select(x => x.FirstOrDefault())
                .ToList();

            return student;
        }

    }

    public partial class GroupController : ControllerBase {
        ILoggerFactory fact;
        public GroupController(ILoggerFactory factory) {
            this.fact = factory;
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
                    Score = 90
                },
                new Student {
                    Course = "en",
                    Score = 90
                },
                new Student {
                    Course = "en",
                    Score = 60
                },
                new Student {
                    Course = "ma",
                    Score = 100
                }
            });
            return context.SaveChanges();
        }


    }
}
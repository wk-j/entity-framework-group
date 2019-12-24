using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace GroupBy.Controllers
{
    public class Student
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; } = "wk";
        public string Course { set; get; }
        public int Score { set; get; }
    }

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students { set; get; }
    }

    [Route("api/[controller]/[action]")]
    public class GroupController : ControllerBase
    {
        DbContextOptions options = new DbContextOptionsBuilder()
            .UseMySql("Host=localhost;User Id=root; Password=1234;Database=App")
            .Options;

        [HttpGet]
        public int Insert()
        {
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
                }
            });
            return context.SaveChanges();
        }

        [HttpGet]
        public IEnumerable<Student> Group()
        {
            using var context = new MyContext(options);
            var student = context.Students
                .ToList()
                .GroupBy(x => x.Course)
                .LastOrDefault();

            return student;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForCSharpTesting.FastDbContextTest;

public record Test1(DateOnly DateOnly, DateTime DateTime)
{
    [Key]
    public int Id { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Test1> Test1 { get; set; }
}

public class FastDbContextTest
{
    public void Test()
    {
        string connectionString = "Host=localhost;Port=32768;Database=postgres_test;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder
            //.UseLazyLoadingProxies()
            .UseNpgsql(connectionString)
            .LogTo(s => Console.WriteLine(s));

        var dbContext = new ApplicationDbContext(optionsBuilder.Options);
        dbContext.Database.EnsureCreated();

        if (!dbContext.Test1.Any())
        {
            var list = new List<Test1>()
            {
                new(new DateOnly(2024, 6, 4), DateTime.UtcNow),
            };

            dbContext.Test1.AddRange(list);
            dbContext.SaveChanges();
        }

        var v1 = dbContext.Test1.ToArray();
    }
}
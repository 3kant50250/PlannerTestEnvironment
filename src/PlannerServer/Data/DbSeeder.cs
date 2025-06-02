using Microsoft.EntityFrameworkCore;
using PlannerServer.Models;

namespace PlannerServer.Data;

public static class DbSeeder
{
    public static void Seed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PlannerDbContext>();

        const int maxRetries = 20;
        int delaySeconds = 5;

        for (int i = 1; i <= maxRetries; i++)
        {
            try
            {
                Console.WriteLine($"[DbSeeder] Attempt {i}: Checking DB connection...");

                context.Database.EnsureCreated();

                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department { Name = "AspIT Østjylland", Street = "Banegårdspladsen 1a, 2.sal", Zip = "8000", City = "Aarhus" },
                        new Department { Name = "AspIT Trekanten", Street = "Boulevarden 48", Zip = "7100", City = "Vejle" }
                    );

                    context.SaveChanges();
                }

                if (!context.Municipalities.Any())
                {
                    context.Municipalities.AddRange(
                        new Municipality { Name = "751" },
                        new Municipality { Name = "630" }
                    );

                    context.SaveChanges();
                }

                if (!context.Students.Any())
                {
                    context.Students.AddRange(
                        new Student { Name = "Nikolaj Rasmussen", Unilogin = "niko2587", DepartmentId = 1, StartDate = DateTime.Now, GraduationDate = DateTime.Now.AddYears(2), MunicipalityId = 1, Birthdate = DateTime.Parse("1996-09-30"), SerialNumber = "8339" },
                        new Student { Name = "Albert Pedersen", Unilogin = "albe3569", DepartmentId = 2, StartDate = DateTime.Now, GraduationDate = DateTime.Now.AddYears(2), MunicipalityId = 1, Birthdate = DateTime.Parse("1997-01-06"), SerialNumber = "5841" },
                        new Student { Name = "Laura Mortensen", Unilogin = "laur4680", DepartmentId = 1, StartDate = DateTime.Now, GraduationDate = DateTime.Now.AddYears(2), MunicipalityId = 1, Birthdate = DateTime.Parse("1997-08-25"), SerialNumber = "1847" },
                        new Student { Name = "Sebastian Eriksen", Unilogin = "seba9870", DepartmentId = 2, StartDate = DateTime.Now, GraduationDate = DateTime.Now.AddYears(2), MunicipalityId = 1, Birthdate = DateTime.Parse("1996-02-15"), SerialNumber = "6725" },
                        new Student { Name = "Julie Kristensen", Unilogin = "juli5698", DepartmentId = 1, StartDate = DateTime.Now, GraduationDate = DateTime.Now.AddYears(2), MunicipalityId = 1, Birthdate = DateTime.Parse("1996-07-23"), SerialNumber = "8133" }
                    );

                    context.SaveChanges();
                }

                //if (!context.Users.Any())
                //{
                //    context.Users.AddRange(
                //        new User { Name = "Alice", Email = "alice@example.com" },
                //        new User { Name = "Bob", Email = "bob@example.com" }
                //    );
                //
                //    context.SaveChanges();
                //}

                Console.WriteLine("[DbSeeder] Database seeded successfully.");
                break; // success
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DbSeeder] Attempt {i} failed: {ex.Message}");

                if (i == maxRetries)
                {
                    Console.WriteLine("[DbSeeder] Max retries reached. Giving up.");
                    break;
                }

                Thread.Sleep(delaySeconds * 1000);
            }
        }
    }
}

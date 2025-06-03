using Microsoft.EntityFrameworkCore;
using PlannerServer.Models;

namespace PlannerServer.Data;

public class DbSeeder
{
    public static async Task SeedAsync(PlannerServerContext context, ILogger logger, CancellationToken cancellationToken = default)
    {
        if (!context.Departments.Any())
        {
            logger.LogInformation("Seeding departments...");

            var departments = new List<Department>
            {
                new Department { Name = "IT", City = "Odense", Zip = "5000", Street = "Tech Street 1" },
                new Department { Name = "Business", City = "Copenhagen", Zip = "2100", Street = "Biz Road 42" }
            };

            await context.Departments.AddRangeAsync(departments, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.Municipalities.Any())
        {
            logger.LogInformation("Seeding municipalities...");

            var municipalities = new List<Municipality>
            {
                new Municipality { Name = "123" }
            };

            await context.Municipalities.AddRangeAsync(municipalities, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.Students.Any())
        {
            logger.LogInformation("Seeding students...");

            var students = new List<Student>
            {
                new Student
                {
                    Name = "Alice Andersen",
                    Unilogin = "alia01",
                    DepartmentId = 1,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate = new DateTime(1999,8, 1),
                    SerialNumber = "1234",
                    IdFromUms = 1,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Bob Bæk",
                    Unilogin = "bobb01",
                    DepartmentId = 2,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(2001, 8, 7),
                    SerialNumber = "5678",
                    IdFromUms = 2,
                    UmsActivity = "Active"
                }
            };

            await context.Students.AddRangeAsync(students, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!context.SchoolItems.Any())
        {
            logger.LogInformation("Seeding school items...");

            var items = new List<SchoolItem>
            {
                new SchoolItem
                {
                    Name = "Laptop HP EliteBook",
                    Description = "14-inch laptop with charger",
                    Number = "IT-001",
                    DepartmentId = 1,
                    StudentId = 1
                },
                new SchoolItem
                {
                    Name = "Monitor Dell 24\"",
                    Description = "1080p monitor",
                    Number = "IT-002",
                    DepartmentId = 1
                },
                new SchoolItem
                {
                    Name = "iPad 10th Gen",
                    Description = "School-issued tablet",
                    Number = "IT-003",
                    DepartmentId = 2,
                    StudentId = 2
                }
            };

            await context.SchoolItems.AddRangeAsync(items, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogInformation("Seeding complete.");
    }
}

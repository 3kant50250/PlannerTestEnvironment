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
                new Department { Name = "AspIT Trekanten", City = "Vejle", Zip = "7100", Street = "Boulevarden 48" },
                new Department { Name = "AspIT Østjylland", City = "Aarhus", Zip = "8000", Street = "Banegårdspladsen 1a, 2.sal" }
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
                    Name = "Sofie Jørgensen",
                    Unilogin = "sofi7892",
                    DepartmentId = 1,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate = new DateTime(1999,8, 1),
                    SerialNumber = "9841",
                    IdFromUms = 1,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Emilie Nielsen",
                    Unilogin = "emil5426",
                    DepartmentId = 1,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(2001, 8, 7),
                    SerialNumber = "3845",
                    IdFromUms = 2,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Frederik Sørensen",
                    Unilogin = "fred6543",
                    DepartmentId = 1,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(1995, 5, 9),
                    SerialNumber = "2135",
                    IdFromUms = 3,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Marie Madsen",
                    Unilogin = "mari1238",
                    DepartmentId = 2,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(1997, 12, 3),
                    SerialNumber = "7429",
                    IdFromUms = 4,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Magnus Johansen",
                    Unilogin = "magn2759",
                    DepartmentId = 2,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(1996, 10, 20),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(1995, 5, 7),
                    SerialNumber = "3179",
                    IdFromUms = 5,
                    UmsActivity = "Active"
                },
                new Student
                {
                    Name = "Benjamin Christensen",
                    Unilogin = "benj4665",
                    DepartmentId = 2,
                    StartDate = new DateTime(2023, 8, 1),
                    GraduationDate = new DateTime(2025, 6, 30),
                    MunicipalityId = 1,
                    Birthdate=new DateTime(1995, 1, 28),
                    SerialNumber = "3457",
                    IdFromUms = 6,
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
                    Name = "Smart phone",
                    Description = "Android phone",
                    Number = "IT-003",
                    DepartmentId = 1,
                    StudentId = 2
                },
                new SchoolItem
                {
                    Name = "Nintendo Switch",
                    Description = "Console",
                    Number = "IT-004",
                    DepartmentId = 1,
                    StudentId = 3
                },
                new SchoolItem
                {
                    Name = "Mario Kart",
                    Description = "Game with box",
                    Number = "IT-005",
                    DepartmentId = 1,
                    StudentId = 3
                },
                new SchoolItem
                {
                    Name = "iPad 10th Gen",
                    Description = "School-issued tablet",
                    Number = "IT-006",
                    DepartmentId = 2,
                    StudentId = 4
                },
                new SchoolItem
                {
                    Name = "Razor mouse",
                    Description = "Pc mouse",
                    Number = "IT-006",
                    DepartmentId = 2,
                    StudentId = 4
                },
                new SchoolItem
                {
                    Name = "Raspberry Pi",
                    Description = "With case",
                    Number = "IT-007",
                    DepartmentId = 2,
                    StudentId = 5
                },
                new SchoolItem
                {
                    Name = "Camera",
                    Description = "Sony",
                    Number = "IT-008",
                    DepartmentId = 2
                }
            };

            await context.SchoolItems.AddRangeAsync(items, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        logger.LogInformation("Seeding complete.");
    }
}

using Microsoft.EntityFrameworkCore;
using PlannerServer.Models;

namespace PlannerServer.Data;

public class DbSeeder
{
    public static void Seed(PlannerServerContext context)
    {
        // Check if the database is already seeded
        if (context.Students.Any())
        {
            return; // DB has already been seeded
        }

        // Sample EnrollmentKinds
        var enrollmentKinds = new List<EnrollmentKind>
            {
                new EnrollmentKind { Name = "UP1" },
                new EnrollmentKind { Name = "UP2" },
                new EnrollmentKind { Name = "AFK" },
                new EnrollmentKind { Name = "Orlov" },
                new EnrollmentKind { Name = "Ordinær" }
            };

        // Sample GraduationKinds
        var graduationKinds = new List<GraduationKind>
            {
                new GraduationKind { Name = "Job" },
                new GraduationKind { Name = "Videre uddannelse" },
                new GraduationKind { Name = "Jobcenter" },
                new GraduationKind { Name = "Andet" }
            };

        // Sample Education types
        var educationList = new List<Education>
            {
                new Education { Name = "AspIT" },
                new Education { Name = "AspIN" }
            };

        // Sample GradtKinds
        var grantKinds = new List<GrantKind>
            {
                new GrantKind { Name = "STU" },
                new GrantKind { Name = "LAB" },
                new GrantKind { Name = "Andet" }
            };

        // Sample Municipalities (unique for each student)
        var municipalities = new List<Municipality>
            {
                new Municipality { Name = "101" },
                new Municipality { Name = "751" },
                new Municipality { Name = "600" },
                new Municipality { Name = "925" },
                new Municipality { Name = "271" },
                new Municipality { Name = "931" },
                new Municipality { Name = "861" },
                new Municipality { Name = "501" },
                new Municipality { Name = "303" },
                new Municipality { Name = "249" },
                new Municipality { Name = "633" },
                new Municipality { Name = "125" },
                new Municipality { Name = "415" },
                new Municipality { Name = "151" },
                new Municipality { Name = "351" },
                new Municipality { Name = "580" },
                new Municipality { Name = "651" },
                new Municipality { Name = "541" },
                new Municipality { Name = "337" },
                new Municipality { Name = "313" },
                new Municipality { Name = "847" },
                new Municipality { Name = "155" },
                new Municipality { Name = "210" },
                new Municipality { Name = "841" },
                new Municipality { Name = "157" },
                new Municipality { Name = "179" }
            };

        context.Municipalities.AddRange(municipalities);
        context.SaveChanges();

        // Add EnrollmentKinds and GraduationKinds
        context.EnrollmentKinds.AddRange(enrollmentKinds);
        context.GraduationKinds.AddRange(graduationKinds);
        context.Educations.AddRange(educationList);
        context.GrantKinds.AddRange(grantKinds);
        context.SaveChanges();

        var departments = new List<Department>
        {
            new Department
            {
                Name = "AspIT Østjylland",
                Street = "Banegårdspladsen 1a, 2.sal",
                Zip = "8000",
                City = "Aarhus",
                Students = new  List<Student>
                {
                    new Student
                    {
                        Name = "Nikolaj Rasmussen",
                        Unilogin = "niko2587",
                        StartDate = DateTime.Now,
                        GraduationDate = DateTime.Now.AddYears(3),
                        MunicipalityId = 1,
                        Birthdate = DateTime.Parse("1996-09-30"),
                        SerialNumber = "8339",
                        IdFromUms = 1,
                        UmsActivity = "Active"
                    },
                    new Student
                    {
                        Name = "Laura Mortensen",
                        Unilogin = "laur4680",
                        StartDate = DateTime.Now,
                        GraduationDate = DateTime.Now.AddYears(3),
                        MunicipalityId = 1,
                        Birthdate = DateTime.Parse("1997-08-25"),
                        SerialNumber = "1847",
                        IdFromUms = 2,
                        UmsActivity = ""
                    },
                    new Student
                    {
                        Name = "Julie Kristensen",
                        Unilogin = "juli5698",
                        StartDate = DateTime.Now,
                        GraduationDate = DateTime.Now.AddYears(3),
                        MunicipalityId = 1,
                        Birthdate = DateTime.Parse("1996-07-23"),
                        SerialNumber = "8133",
                        IdFromUms = 3,
                        UmsActivity = "Active"
                    }
                },
                Users = new List<User>
                {
                    new User
                    {
                        UserName = "Nikolaj",
                        Initials = "Ni",
                        IsActive = true,
                        Level = 1,
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Team Nikolaj",
                                Module = "IT"
                            }
                        }
                    },
                    new User
                    {
                        UserName = "Laura",
                        Initials = "La",
                        IsActive = true,
                        Level = 1,
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Team Laura",
                                Module = "IT"
                            }
                        }
                    },
                    new User
                    {
                        UserName = "Julie",
                        Initials = "Ju",
                        IsActive = true,
                        Level = 1,
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Team Julie",
                                Module = "IT"
                            }
                        }
                    }
                }
            },
            new Department
            {
                Name = "AspIT Trekanten",
                Street = "Boulevarden 48",
                Zip = "7100",
                City = "Vejle",
                Students = new  List<Student>
                {
                    new Student
                    {
                        Name = "Albert Pedersen",
                        Unilogin = "albe3569",
                        StartDate = DateTime.Now,
                        GraduationDate = DateTime.Now.AddYears(3),
                        MunicipalityId = 1,
                        Birthdate = DateTime.Parse("1997-01-06"),
                        SerialNumber = "5841",
                        IdFromUms = 4,
                        UmsActivity = "Active"
                    },
                    new Student
                    {
                        Name = "Sebastian Erikse",
                        Unilogin = "seba9870",
                        StartDate = DateTime.Now,
                        GraduationDate = DateTime.Now.AddYears(3),
                        MunicipalityId = 1,
                        Birthdate = DateTime.Parse("1996-02-15"),
                        SerialNumber = "6725",
                        IdFromUms = 5,
                        UmsActivity = "Active"
                    }
                },
                Users = new List<User>
                {
                    new User
                    {
                        UserName = "Albert",
                        Initials = "Al",
                        IsActive = true,
                        Level = 1,
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Team Albert",
                                Module = "IT",

                            }
                        }
                    },
                    new User
                    {
                        UserName = "Sebastian",
                        Initials = "Se",
                        IsActive = true,
                        Level = 1,
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Team Sebastian",
                                Module = "IT"
                            }
                        }
                    }
                }
            }
        };
        context.Departments.AddRange(departments);
        context.SaveChanges();
    }
}

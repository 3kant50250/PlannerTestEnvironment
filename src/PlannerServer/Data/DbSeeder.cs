using Microsoft.EntityFrameworkCore;
using PlannerServer.Model;

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

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Name = "Alice", Email = "alice@example.com" },
                        new User { Name = "Bob", Email = "bob@example.com" }
                    );

                    context.SaveChanges();
                }

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

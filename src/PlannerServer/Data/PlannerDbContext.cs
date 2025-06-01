using Microsoft.EntityFrameworkCore;
using PlannerServer.Model;

namespace PlannerServer.Data
{
    public class PlannerDbContext : DbContext
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}

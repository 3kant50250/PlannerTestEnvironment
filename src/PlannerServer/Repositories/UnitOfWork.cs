using PlannerServer.Data;
using PlannerServer.Repositories.Interfaces;

namespace PlannerServer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PlannerDbContext _context;

        // Loggers
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILogger<StudentRepository> _studentRepositoryLogger;

        // Repositories
        private readonly IStudentRepository _studentRepository;

        public UnitOfWork(PlannerDbContext context,
            ILogger<UnitOfWork> logger,
            ILogger<StudentRepository> studentRepositoryLogger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _studentRepositoryLogger = studentRepositoryLogger ?? throw new ArgumentNullException(nameof(studentRepositoryLogger));

            _studentRepository = new StudentRepository(_context, _studentRepositoryLogger);
        }

        // Property access for repositories
        public IStudentRepository StudentRepository => _studentRepository;

        // Complete unit of work and save changes asynchronously
        public async Task<bool> CompleteAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Saving changes to the database.");
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
                return false;
            }
        }

        // Dispose of the context when done
        public void Dispose()
        {
            _logger.LogInformation("Disposing of the UnitOfWork.");
            _context?.Dispose();
        }
    }
}

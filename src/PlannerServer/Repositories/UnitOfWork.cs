using PlannerServer.Data;
using PlannerServer.Repositories.Interfaces;

namespace PlannerServer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PlannerServerContext _context;

        // Loggers
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ILogger<StudentRepository> _studentRepositoryLogger;
        private readonly ILogger<SchoolItemRepository> _schoolItemRepositoryLogger;

        // Repositories
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolItemRepository _schoolItemRepository;

        public UnitOfWork(PlannerServerContext context,
            ILogger<UnitOfWork> logger,
            ILogger<StudentRepository> studentRepositoryLogger,
            ILogger<SchoolItemRepository> schoolItemRepositoryLogger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _studentRepositoryLogger = studentRepositoryLogger ?? throw new ArgumentNullException(nameof(studentRepositoryLogger));
            _schoolItemRepositoryLogger = schoolItemRepositoryLogger ?? throw new ArgumentNullException(nameof(schoolItemRepositoryLogger));
            
            _studentRepository = new StudentRepository(_context, _studentRepositoryLogger);
            _schoolItemRepository = new SchoolItemRepository(_context, _schoolItemRepositoryLogger);
        }

        // Property access for repositories
        public IStudentRepository StudentRepository => _studentRepository;
        public ISchoolItemRepository SchoolItemRepository => _schoolItemRepository;

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

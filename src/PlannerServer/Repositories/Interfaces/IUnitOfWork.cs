namespace PlannerServer.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Provides access to Student repository.
        IStudentRepository StudentRepository { get; }
        // Provides access to SchoolItem repository.
        ISchoolItemRepository SchoolItemRepository { get; }

        // Completes the unit of work and commits changes to the database.
        Task<bool> CompleteAsync(CancellationToken cancellationToken);
    }
}

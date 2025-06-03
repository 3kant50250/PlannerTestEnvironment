using PlannerServer.Enums;
using PlannerServer.Models;

namespace PlannerServer.Repositories.Interfaces
{
    public interface ISchoolItemRepository
    {
        // Retrieves a SchoolItem by their unique identifier.
        Task<SchoolItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
        // Retrieves all SchoolItems in the system.
        Task<IEnumerable<SchoolItem>> GetAllAsync(CancellationToken cancellationToken);
        // Adds a new SchoolItem to the system.
        Task<OperationResult> AddAsync(SchoolItem SchoolItem, CancellationToken cancellationToken);
        // Updates the details of an existing SchoolItem.
        Task<OperationResult> UpdateAsync(SchoolItem SchoolItem, CancellationToken cancellationToken);
        // Deletes a SchoolItem by their unique identifier.
        Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

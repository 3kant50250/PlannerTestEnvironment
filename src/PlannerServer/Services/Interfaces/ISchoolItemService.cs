using PlannerServer.Enums;
using PlannerServer.Models;

namespace PlannerServer.Services.Interfaces
{
    public interface ISchoolItemService
    {
        Task<SchoolItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<SchoolItem>> GetAllSchoolItemsAsync(CancellationToken cancellationToken);
        Task<OperationResult> AddSchoolItemAsync(SchoolItem schoolItem, CancellationToken cancellationToken);
        Task<OperationResult> UpdateSchoolItemAsync(int id, SchoolItem schoolItem, CancellationToken cancellationToken);
        Task<OperationResult> DeleteSchoolItemAsync(int id, CancellationToken cancellationToken);
        Task<OperationResult> RegisterItemToStudent(int schoolItemId, int studentId, CancellationToken cancellationToken);
        Task<OperationResult> DeregisterItemFromStudentAsync(int schoolItemId, CancellationToken cancellationToken);
    }
}

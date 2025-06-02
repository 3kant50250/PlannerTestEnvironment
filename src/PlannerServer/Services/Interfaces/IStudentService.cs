using PlannerServer.Enums;
using PlannerServer.Models;

namespace PlannerServer.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<OperationResult> AddStudentAsync(Student student, CancellationToken cancellationToken);
        Task<OperationResult> UpdateStudentAsync(int id, Student student, CancellationToken cancellationToken);
        Task<OperationResult> DeleteStudentAsync(int id, CancellationToken cancellationToken);
    }
}

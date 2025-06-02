using PlannerServer.Enums;
using PlannerServer.Models;

namespace PlannerServer.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        // Retrieves a student by their unique identifier.
        Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken);
        // Retrieves all students in the system.
        Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken);
        // Adds a new student to the system.
        Task<OperationResult> AddAsync(Student student, CancellationToken cancellationToken);
        // Updates the details of an existing student.
        Task<OperationResult> UpdateAsync(Student student, CancellationToken cancellationToken);
        // Deletes a student by their unique identifier.
        Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

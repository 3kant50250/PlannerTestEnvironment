using Microsoft.EntityFrameworkCore;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Repositories.Interfaces;
using PlannerServer.Services.Interfaces;

namespace PlannerServer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Helper method to handle saving and completing actions
        private async Task<OperationResult> SaveChangesAsync()
        {
            var saveResult = await _unitOfWork.CompleteAsync(CancellationToken.None);
            return saveResult ? OperationResult.Success : OperationResult.Failure;
        }

        // Retrieves a student by ID
        public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching student with ID {StudentId}.", id);

            try
            {
                var student = await _unitOfWork.StudentRepository
                    .GetByIdAsync(id, cancellationToken);

                if (student == null)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found.", id);
                }

                return student;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while fetching student with ID {StudentId}.", id);
                throw new DataAccessException($"ArgumentNullException occurred while fetching student with ID {id}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while fetching student with ID {StudentId}.", id);
                throw new DataAccessException($"Operation was canceled while fetching student with ID {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching student with ID {StudentId}.", id);
                throw new DataAccessException($"An unexpected error occurred while fetching student with ID {id}.", ex);
            }
        }

        // Retrieves all students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all students.");

            try
            {
                var students = await _unitOfWork.StudentRepository
                    .GetAllAsync(cancellationToken);

                if (!students.Any())
                {
                    _logger.LogWarning("No students found.");
                }

                return students;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while retrieving all students.");
                throw new DataAccessException("ArgumentNullException occurred while retrieving all students.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while retrieving all students.");
                throw new DataAccessException("Operation was canceled while retrieving all students.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all students.");
                throw new DataAccessException("An unexpected error occurred while retrieving all students.", ex);
            }
        }

        // Adds a new student
        public async Task<OperationResult> AddStudentAsync(Student student, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding student with ID {StudentId}.", student.StudentId);

            if (student == null)
            {
                _logger.LogWarning("Attempting to add a null student.");
                return OperationResult.UnexpectedError;
            }

            try
            {
                var result = await _unitOfWork.StudentRepository
                    .AddAsync(student, cancellationToken);

                if (result != OperationResult.Success)
                {
                    return result;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after adding student with ID {StudentId}.", student.StudentId);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"DbUpdateException occurred while adding student with ID {student.StudentId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while adding student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"Operation was canceled while adding student with ID {student.StudentId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"An unexpected error occurred while adding student with ID {student.StudentId}.", ex);
            }
        }

        // Updates an existing student
        public async Task<OperationResult> UpdateStudentAsync(int id, Student student, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating student with ID {StudentId}.", student.StudentId);

            if (student == null)
            {
                _logger.LogWarning("Attempting to update a null student.");
                return OperationResult.UnexpectedError;
            }

            if (id != student.StudentId)
            {
                _logger.LogWarning("ID mismatch during update. Provided ID: {ProvidedId}, Student ID: {StudentId}", id, student.StudentId);
                return OperationResult.IDMismatch;
            }

            try
            {
                var result = await _unitOfWork.StudentRepository
                    .UpdateAsync(student, cancellationToken);

                if (result != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to update student with ID {StudentId}.", student.StudentId);
                    return result;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after updating student with ID {StudentId}.", student.StudentId);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while updating student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"DbUpdateException occurred while updating student with ID {student.StudentId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while updating student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"Operation was canceled while updating student with ID {student.StudentId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating student with ID {StudentId}.", student.StudentId);
                throw new DataAccessException($"An unexpected error occurred while updating student with ID {student.StudentId}.", ex);
            }
        }

        // Deletes an existing student by ID
        public async Task<OperationResult> DeleteStudentAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete student with ID {StudentId}.", id);

            try
            {
                var result = await _unitOfWork.StudentRepository
                    .DeleteAsync(id, cancellationToken);

                if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found for deletion.", id);
                    return OperationResult.NotFound;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after deleting student with ID {StudentId}.", id);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while deleting student with ID {StudentId}.", id);
                throw new DataAccessException($"DbUpdateException occurred while deleting student with ID {id}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while deleting student with ID {StudentId}.", id);
                throw new DataAccessException($"Operation was canceled while deleting student with ID {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting student with ID {StudentId}.", id);
                throw new DataAccessException("An unexpected error occurred while deleting the student.", ex);
            }
        }
    }
}
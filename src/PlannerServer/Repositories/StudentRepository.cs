using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlannerServer.Data;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Repositories.Interfaces;

namespace PlannerServer.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PlannerServerContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(PlannerServerContext context, ILogger<StudentRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Retrieves a student by ID
        public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve student with ID {StudentId}.", id);

            try
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.StudentId == id, cancellationToken);

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
        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve all students.");

            try
            {
                var students = await _context.Students
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved {StudentCount} students.", students.Count);
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

        // Add a student to the context
        public async Task<OperationResult> AddAsync(Student student, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to add a new student with ID {StudentId}.", student.StudentId);

            try
            {
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.SerialNumber == student.SerialNumber, cancellationToken);

                if (existingStudent != null)
                {
                    _logger.LogWarning("Student with SerialNumber {SerialNumber} already exists.", student.SerialNumber);
                    return OperationResult.Failure;
                }

                await _context.Students
                    .AddAsync(student, cancellationToken);

                _logger.LogInformation("Student with SerialNumber {SerialNumber} already exists.", student.SerialNumber);
                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding student with SerialNumber {SerialNumber}.", student.SerialNumber);
                throw new DataAccessException($"DbUpdateException occurred while adding student with SerialNumber {student.SerialNumber}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "DbUpdateException occurred while adding student with SerialNumber {SerialNumber}.", student.SerialNumber);
                throw new DataAccessException($"DbUpdateException occurred while adding student with SerialNumber  {student.SerialNumber}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding student with SerialNumber {SerialNumber}.", student.SerialNumber);
                throw new DataAccessException($"DbUpdateException occurred while adding student with SerialNumber  {student.SerialNumber}.", ex);
            }
        }

        // Update an existing student
        public async Task<OperationResult> UpdateAsync(Student student, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to update student with ID {StudentId}.", student.StudentId);

            try
            {
                var existingStudent = await _context.Students
                    .FindAsync([student.StudentId], cancellationToken);

                if (existingStudent == null)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found for update.", student.StudentId);
                    return OperationResult.NotFound;
                }

                _context.Entry(existingStudent).CurrentValues
                    .SetValues(student);

                _logger.LogInformation("Successfully updated student with ID {StudentId}.", student.StudentId);
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

        // Delete a student by ID
        public async Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete student with ID {StudentId}.", id);

            try
            {
                var existingStudent = await _context.Students
                    .FindAsync([id], cancellationToken);

                if (existingStudent == null)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found for deletion.", id);
                    return OperationResult.NotFound;
                }

                _context.Students
                    .Remove(existingStudent);

                _logger.LogInformation("Successfully deleted student with ID {StudentId}.", id);
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
                throw new DataAccessException($"An unexpected error occurred while deleting student with ID {id}.", ex);
            }
        }
    }
}

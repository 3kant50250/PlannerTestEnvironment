using Microsoft.EntityFrameworkCore;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Repositories.Interfaces;
using PlannerServer.Services.Interfaces;

namespace PlannerServer.Services
{
    public class SchoolItemService : ISchoolItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SchoolItem> _logger;

        public SchoolItemService(IUnitOfWork unitOfWork, ILogger<SchoolItem> logger)
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

        // Retrieves a school item by ID
        public async Task<SchoolItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching school item with ID {SchoolItemId}.", id);

            try
            {
                var schoolItem = await _unitOfWork.SchoolItemRepository
                    .GetByIdAsync(id, cancellationToken);

                if (schoolItem == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", id);
                }

                return schoolItem;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while fetching school item with ID {SchoolItemId}.", id);
                throw new DataAccessException($"ArgumentNullException occurred while fetching school item with ID {id}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while fetching school item with ID {SchoolItemId}.", id);
                throw new DataAccessException($"Operation was canceled while fetching school item with ID {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching school item with ID {SchoolItemId}.", id);
                throw new DataAccessException($"An unexpected error occurred while fetching school item with ID {id}.", ex);
            }
        }

        // Retrieves all schooll items
        public async Task<IEnumerable<SchoolItem>> GetAllSchoolItemsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all schooll items.");

            try
            {
                var schoolItems = await _unitOfWork.SchoolItemRepository
                    .GetAllAsync(cancellationToken);

                if (!schoolItems.Any())
                {
                    _logger.LogWarning("No schooll items found.");
                }

                return schoolItems;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while retrieving all schooll items.");
                throw new DataAccessException("ArgumentNullException occurred while retrieving all schooll items.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while retrieving all schooll items.");
                throw new DataAccessException("Operation was canceled while retrieving all schooll items.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all schooll items.");
                throw new DataAccessException("An unexpected error occurred while retrieving all schooll items.", ex);
            }
        }

        // Adds a new school item
        public async Task<OperationResult> AddSchoolItemAsync(SchoolItem schoolItem, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);

            if (schoolItem == null)
            {
                _logger.LogWarning("Attempting to add a null school item.");
                return OperationResult.UnexpectedError;
            }

            try
            {
                var result = await _unitOfWork.SchoolItemRepository
                    .AddAsync(schoolItem, cancellationToken);

                if (result != OperationResult.Success)
                {
                    return result;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after adding school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"DbUpdateException occurred while adding school item with ID {schoolItem.SchoolItemId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while adding school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"Operation was canceled while adding school item with ID {schoolItem.SchoolItemId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"An unexpected error occurred while adding school item with ID {schoolItem.SchoolItemId}.", ex);
            }
        }

        // Updates an existing school item
        public async Task<OperationResult> UpdateSchoolItemAsync(int id, SchoolItem schoolItem, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);

            if (schoolItem == null)
            {
                _logger.LogWarning("Attempting to update a null school item.");
                return OperationResult.UnexpectedError;
            }

            if (id != schoolItem.SchoolItemId)
            {
                _logger.LogWarning("ID mismatch during update. Provided ID: {ProvidedId}, school item ID: {SchoolItemId}", id, schoolItem.SchoolItemId);
                return OperationResult.IDMismatch;
            }

            try
            {
                var result = await _unitOfWork.SchoolItemRepository
                    .UpdateAsync(schoolItem, cancellationToken);

                if (result != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to update school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                    return result;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after updating school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while updating school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"DbUpdateException occurred while updating school item with ID {schoolItem.SchoolItemId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while updating school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"Operation was canceled while updating school item with ID {schoolItem.SchoolItemId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
                throw new DataAccessException($"An unexpected error occurred while updating school item with ID {schoolItem.SchoolItemId}.", ex);
            }
        }

        // Deletes an existing school item by ID
        public async Task<OperationResult> DeleteSchoolItemAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete school item with ID {SchoolItemId}.", id);

            try
            {
                var result = await _unitOfWork.SchoolItemRepository
                    .DeleteAsync(id, cancellationToken);

                if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found for deletion.", id);
                    return OperationResult.NotFound;
                }

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after deleting school item with ID {SchoolItemId}.", id);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while deleting school item with ID {SchoolItemId}.", id);
                throw new DataAccessException($"DbUpdateException occurred while deleting school item with ID {id}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while deleting school item with ID {SchoolItemId}.", id);
                throw new DataAccessException($"Operation was canceled while deleting school item with ID {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting school item with ID {SchoolItemId}.", id);
                throw new DataAccessException("An unexpected error occurred while deleting the school item.", ex);
            }
        }

        // Register an item to a student
        public async Task<OperationResult> RegisterItemToStudent(int schoolItemId, int studentId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to register school item with ID {SchoolItemId} to student with ID {StudentId}.", schoolItemId, studentId);

            try
            {
                var item = await _unitOfWork.SchoolItemRepository
                        .GetByIdAsync(schoolItemId, cancellationToken);

                if (item == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", schoolItemId);
                    return OperationResult.Failure;
                }

                if (item.StudentId.HasValue)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} already is assigned a student.", schoolItemId);
                    return OperationResult.Failure;
                }

                var student = await _unitOfWork.StudentRepository
                        .GetByIdAsync(studentId, cancellationToken);

                if (student == null)
                {
                    return OperationResult.Failure;
                }

                if (item.DepartmentId != student.DepartmentId)
                {
                    return OperationResult.Failure;
                }

                item.StudentId = student.StudentId;
                student.SchoolItems.Add(item);

                var saveResult = await SaveChangesAsync();

                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes.");
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while registering school item with ID {SchoolItemId} to student with ID {StudentId}.", schoolItemId, studentId);
                throw new DataAccessException($"DbUpdateException occurred while registering school item with ID {schoolItemId} to student with {studentId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while registering school item with ID {SchoolItemId} to student with ID {StudentId}.", schoolItemId, studentId);
                throw new DataAccessException($"Operation was canceled while registering school item with ID {schoolItemId} to student with {studentId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while registering school item with ID {SchoolItemId} to student with ID {StudentId}.", schoolItemId, studentId);
                throw new DataAccessException("An unexpected error occurred while registering the school item to a student.", ex);
            }
        }

        // Register an item to a student
        public async Task<OperationResult> DeregisterItemFromStudentAsync(int schoolItemId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to deregister school item with ID {SchoolItemId} from its student.", schoolItemId);

            try
            {
                var item = await _unitOfWork.SchoolItemRepository
                        .GetByIdAsync(schoolItemId, cancellationToken);

                if (item == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", schoolItemId);
                    return OperationResult.Failure;
                }

                if (!item.StudentId.HasValue)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} is not currently assigned to any student.", schoolItemId);
                    return OperationResult.Failure;
                }

                var student = await _unitOfWork.StudentRepository
                        .GetByIdAsync(item.StudentId.Value, cancellationToken);

                if (student != null)
                {
                    student.SchoolItems.Remove(item);
                }

                item.StudentId = null;

                var saveResult = await SaveChangesAsync();
                if (saveResult != OperationResult.Success)
                {
                    _logger.LogWarning("Failed to save changes after deregistering school item with ID {SchoolItemId}.", schoolItemId);
                    return saveResult;
                }

                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while deregistering school item with ID {SchoolItemId}.", schoolItemId);
                throw new DataAccessException($"DbUpdateException occurred while deregistering school item with ID {schoolItemId}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while deregistering school item with ID {SchoolItemId}.", schoolItemId);
                throw new DataAccessException($"Operation was canceled while deregistering school item with ID {schoolItemId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deregistering school item with ID {SchoolItemId}.", schoolItemId);
                throw new DataAccessException("An unexpected error occurred while deregistering the school item.", ex);
            }
        }

    }
}

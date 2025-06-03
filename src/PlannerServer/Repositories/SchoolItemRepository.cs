using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlannerServer.Data;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Repositories.Interfaces;

namespace PlannerServer.Repositories
{
    public class SchoolItemRepository : ISchoolItemRepository
    {
        private readonly PlannerServerContext _context;
        private readonly ILogger<SchoolItemRepository> _logger;

        public SchoolItemRepository(PlannerServerContext context, ILogger<SchoolItemRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Retrieves a school item by ID
        public async Task<SchoolItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve school item with ID {SchoolItemId}.", id);

            try
            {
                var schoolItem = await _context.SchoolItems
                    .FirstOrDefaultAsync(s => s.SchoolItemId == id, cancellationToken);

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

        // Retrieves all school items
        public async Task<IEnumerable<SchoolItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve all school items.");

            try
            {
                var schoolItems = await _context.SchoolItems
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved {SchoolItemCount} school items.", schoolItems.Count);
                return schoolItems;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "ArgumentNullException occurred while retrieving all school items.");
                throw new DataAccessException("ArgumentNullException occurred while retrieving all school items.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Operation was canceled while retrieving all school items.");
                throw new DataAccessException("Operation was canceled while retrieving all school items.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all school items.");
                throw new DataAccessException("An unexpected error occurred while retrieving all school items.", ex);
            }
        }

        // Add a school item to the context
        public async Task<OperationResult> AddAsync(SchoolItem schoolItem, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to add a new school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);

            try
            {
                var existingSchoolItem = await _context.SchoolItems
                    .FirstOrDefaultAsync(s => s.Number == schoolItem.Number, cancellationToken);

                if (existingSchoolItem != null)
                {
                    _logger.LogWarning("School item with Number {Number} already exists.", schoolItem.Number);
                    return OperationResult.Failure;
                }

                await _context.SchoolItems
                    .AddAsync(schoolItem, cancellationToken);

                _logger.LogInformation("School item with Number {Number} already exists.", schoolItem.Number);
                return OperationResult.Success;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding school item with Number {Number}.", schoolItem.Number);
                throw new DataAccessException($"DbUpdateException occurred while adding school item with Number {schoolItem.Number}.", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "DbUpdateException occurred while adding school item with Number {Number}.", schoolItem.Number);
                throw new DataAccessException($"DbUpdateException occurred while adding school item with Number  {schoolItem.Number}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DbUpdateException occurred while adding school item with Number {Number}.", schoolItem.Number);
                throw new DataAccessException($"DbUpdateException occurred while adding school item with Number  {schoolItem.Number}.", ex);
            }
        }

        // Update an existing school item
        public async Task<OperationResult> UpdateAsync(SchoolItem schoolItem, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to update school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);

            try
            {
                var existingSchoolItem = await _context.SchoolItems
                    .FindAsync([schoolItem.SchoolItemId], cancellationToken);

                if (existingSchoolItem == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found for update.", schoolItem.SchoolItemId);
                    return OperationResult.NotFound;
                }

                _context.Entry(existingSchoolItem).CurrentValues
                    .SetValues(schoolItem);

                _logger.LogInformation("Successfully updated school item with ID {SchoolItemId}.", schoolItem.SchoolItemId);
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

        // Delete a school item by ID
        public async Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete school item with ID {SchoolItemId}.", id);

            try
            {
                var existingSchoolItem = await _context.SchoolItems
                    .FindAsync([id], cancellationToken);

                if (existingSchoolItem == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found for deletion.", id);
                    return OperationResult.NotFound;
                }

                _context.SchoolItems
                    .Remove(existingSchoolItem);

                _logger.LogInformation("Successfully deleted school item with ID {SchoolItemId}.", id);
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
                throw new DataAccessException($"An unexpected error occurred while deleting school item with ID {id}.", ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PlannerServer.Dto.Get;
using PlannerServer.Dto.Post;
using PlannerServer.Dto.Put;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Services;
using PlannerServer.Services.Interfaces;

namespace PlannerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolItemsController : ControllerBase
    {
        private readonly ISchoolItemService _schoolItemService;
        private readonly ILogger<SchoolItemsController> _logger;

        public SchoolItemsController(ISchoolItemService schoolItemService, ILogger<SchoolItemsController> logger)
        {
            _schoolItemService = schoolItemService ?? throw new ArgumentNullException(nameof(schoolItemService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolItemReadDto>>> GetAllitems(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all items.");

            try
            {
                var schoolItems = await _schoolItemService
                    .GetAllSchoolItemsAsync(cancellationToken);

                if (!schoolItems.Any())
                {
                    _logger.LogWarning("No items found.");
                    return NotFound("No items found.");
                }

                var schoolItemDtos = schoolItems.Select(item => new SchoolItemReadDto
                {
                    SchoolItemId = item.SchoolItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Number = item.Number,
                    DepartmentId = item.DepartmentId,
                    StudentId = item.StudentId,
                    DepartmentName = item.Department.Name,
                    StudentName = item.Student?.Name
                });

                _logger.LogInformation("Successfully fetched items.");
                return Ok(schoolItemDtos);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching items.");
                return StatusCode(500, "An error occurred while retrieving the items.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching items.");
                return StatusCode(500, "An unexpected error occurred while retrieving the items.");
            }
        }

        // GET: api/schoolitems
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolItemReadDto>> GetSchoolItemById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting school item with ID {SchoolItemId}.", id);

            try
            {
                var item = await _schoolItemService
                    .GetByIdAsync(id, cancellationToken);

                if (item == null)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", id);
                    return NotFound($"School item with ID {id} not found.");
                }

                var dto = new SchoolItemReadDto
                {
                    SchoolItemId = item.SchoolItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Number = item.Number,
                    DepartmentId = item.DepartmentId,
                    StudentId = item.StudentId
                };

                _logger.LogInformation("Successfully fetched school item with ID {SchoolItemId}.", item.SchoolItemId);
                return Ok(dto);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while fetching school item with ID {SchoolItemId}.", id);
                return StatusCode(500, $"An error occurred while retrieving school item with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching school item with ID {SchoolItemId}.", id);
                return StatusCode(500, $"An unexpected error occurred while retrieving school item with ID {id}.");
            }
        }

        // PUT: api/schoolitems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchoolItem(int id, SchoolItemUpdateDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating school item with ID {SchoolItemId}.", id);

            var item = new SchoolItem
            {
                Name = dto.Name,
                Description = dto.Description,               
                Number = dto.Number,
                DepartmentId = dto.DepartmentId,
                StudentId = dto.StudentId
            };

            try
            {
                var result = await _schoolItemService
                    .UpdateSchoolItemAsync(id, item, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully updatedschool item with ID {SchoolItemId}.", id);
                    return Ok(item);
                }
                else if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", id);
                    return NotFound($"School item with ID {id} not found.");
                }
                else if (result == OperationResult.IDMismatch)
                {
                    _logger.LogWarning("School item ID mismatch. Provided ID: {ProvidedId},school item ID: {SchoolItemId}", id, item.SchoolItemId);
                    return BadRequest($"School item ID mismatch. Provided ID: {id},school item ID: {item.SchoolItemId}");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while updating school item with ID {SchoolItemId}.", id);
                    return StatusCode(500, "An unexpected error occurred while updating the school item.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while updating school item with ID {SchoolItemId}.", id);
                return StatusCode(500, "An error occurred while updating the school item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating school item with ID {SchoolItemId}.", id);
                return StatusCode(500, "An unexpected error occurred while updating the school item.");
            }
        }

        // POST: api/schoolitems
        [HttpPost]
        public async Task<ActionResult<SchoolItem>> CreateSchoolItem(SchoolItemCreateDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating school item with Name {Name}.", dto.Name);

            try
            {
                var item = new SchoolItem
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Number = dto.Number,
                    DepartmentId = dto.DepartmentId
                };

                var result = await _schoolItemService
                    .AddSchoolItemAsync(item, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully created school item with ID {SchoolItemId}.", item.SchoolItemId);
                    return CreatedAtAction(nameof(GetSchoolItemById), new { id = item.SchoolItemId }, item);
                }
                else if (result == OperationResult.Failure)
                {
                    _logger.LogWarning("Failed to create school item with Number{Number}.", dto.Number);
                    return BadRequest("Failed to create the school item.");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while creating school item with ID {SchoolItemId}.", item.SchoolItemId);
                    return StatusCode(500, "An unexpected error occurred while creating the school item.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while creating school item with Number{Number}.", dto.Number);
                return StatusCode(500, "An error occurred while creating the school item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating school item with Number{Number}.", dto.Number);
                return StatusCode(500, "An unexpected error occurred while creating the school item.");
            }
        }

        [HttpPost("{itemId}/assign/{studentId}")]
        public async Task<IActionResult> AssignToStudent(int itemId, int studentId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assign school item with ID {SchoolItemId} to student with ID {StudentID}.", itemId, studentId);

            try
            {
                var result = await _schoolItemService.RegisterItemToStudent(itemId, studentId, cancellationToken);
                if (result != OperationResult.Success)
                {
                    return BadRequest(new { message = "Could not assign item to student." });
                }

                return Ok(new { message = "Item assigned to student successfully." });
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while asigning school item with ID {SchoolItemId}.", itemId);
                return StatusCode(500, "An error occurred while asigning the school item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while asigning school item with ID {SchoolItemId}.", itemId);
                return StatusCode(500, "An unexpected error occurred while asigning the school item.");
            }
        }

        [HttpPost("{itemId}/unassign")]
        public async Task<IActionResult> UnassignFromStudent(int itemId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unassign school item with ID {SchoolItemId}.", itemId);

            try
            {
                var result = await _schoolItemService.DeregisterItemFromStudentAsync(itemId, cancellationToken);
                if (result != OperationResult.Success)
                {
                    return BadRequest(new { message = "Could not unassign item from student." });
                }

                return Ok(new { message = "Item unassigned from student successfully." });
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while unsigning school item with ID {SchoolItemId}.", itemId);
                return StatusCode(500, "An error occurred while unsigning the school item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while unsigning school item with ID {SchoolItemId}.", itemId);
                return StatusCode(500, "An unexpected error occurred while unsigning the school item.");
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolItem(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting school item with ID {SchoolItemId}.", id);

            try
            {
                var result = await _schoolItemService
                    .DeleteSchoolItemAsync(id, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully deleted school item with ID {SchoolItemId}.", id);
                    return NoContent();
                }
                else if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("School item with ID {SchoolItemId} not found.", id);
                    return NotFound($"School item with ID {id} not found.");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while deleting school item with ID {SchoolItemId}.", id);
                    return StatusCode(500, "An unexpected error occurred while deleting the school item.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while deleting school item with ID {SchoolItemId}.", id);
                return StatusCode(500, "An error occurred while deleting the school item.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting school item with ID {SchoolItemId}.", id);
                return StatusCode(500, "An unexpected error occurred while deleting the school item.");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PlannerServer.Dto.Get;
using PlannerServer.Dto.Post;
using PlannerServer.Dto.Put;
using PlannerServer.Enums;
using PlannerServer.Exceptions;
using PlannerServer.Models;
using PlannerServer.Services.Interfaces;

namespace PlannerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetAllStudents(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all students.");

            try
            {
                var students = await _studentService
                    .GetAllStudentsAsync(cancellationToken);

                if (!students.Any())
                {
                    _logger.LogWarning("No students found.");
                    return NotFound("No students found.");
                }

                var studentDtos = students.Select(student => new StudentReadDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    Unilogin = student.Unilogin,
                    Birthdate = student.Birthdate,
                    StartDate = student.StartDate,
                    GraduationDate = student.GraduationDate,
                    DepartmentId = student.DepartmentId,
                    MunicipalityId = student.MunicipalityId,
                    SerialNumber = student.SerialNumber,
                    UmsActivity = student.UmsActivity
                });

                _logger.LogInformation("Successfully fetched students.");
                return Ok(studentDtos);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "Error occurred while fetching students.");
                return StatusCode(500, "An error occurred while retrieving the students.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching students.");
                return StatusCode(500, "An unexpected error occurred while retrieving the students.");
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudentById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting student with ID {StudentId}.", id);

            try
            {
                var student = await _studentService
                    .GetByIdAsync(id, cancellationToken);

                if (student == null)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found.", id);
                    return NotFound($"Student with ID {id} not found.");
                }

                var dto = new StudentReadDto
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    Unilogin = student.Unilogin,
                    StartDate = student.StartDate,
                    GraduationDate = student.GraduationDate,
                    Birthdate = student.Birthdate,
                    SerialNumber = student.SerialNumber,
                    UmsActivity = student.UmsActivity,
                    DepartmentId = student.DepartmentId,
                    MunicipalityId = student.MunicipalityId
                };

                _logger.LogInformation("Successfully fetched student with ID {StudentId}.", student.StudentId);
                return Ok(dto);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while fetching student with ID {StudentId}.", id);
                return StatusCode(500, $"An error occurred while retrieving student with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching student with ID {StudentId}.", id);
                return StatusCode(500, $"An unexpected error occurred while retrieving student with ID {id}.");
            }
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating student with ID {StudentId}.", id);

            var student = new Student
            {
                StudentId = dto.StudentId,
                Name = dto.Name,
                Unilogin = dto.Unilogin,
                DepartmentId = dto.DepartmentId,
                MunicipalityId = dto.MunicipalityId,
                StartDate = dto.StartDate,
                GraduationDate = dto.GraduationDate,
                Birthdate = dto.Birthdate,
                SerialNumber = dto.SerialNumber,
                IdFromUms = dto.IdFromUms,
                UmsActivity = dto.UmsActivity
            };

            try
            {
                var result = await _studentService
                    .UpdateStudentAsync(id, student, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully updated student with ID {StudentId}.", id);
                    return Ok(student);
                }
                else if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found.", id);
                    return NotFound($"Student with ID {id} not found.");
                }
                else if (result == OperationResult.IDMismatch)
                {
                    _logger.LogWarning("Student ID mismatch. Provided ID: {ProvidedId}, Student ID: {StudentId}", id, student.StudentId);
                    return BadRequest($"Student ID mismatch. Provided ID: {id}, Student ID: {student.StudentId}");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while updating student with ID {StudentId}.", id);
                    return StatusCode(500, "An unexpected error occurred while updating the student.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while updating student with ID {StudentId}.", id);
                return StatusCode(500, "An error occurred while updating the student.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating student with ID {StudentId}.", id);
                return StatusCode(500, "An unexpected error occurred while updating the student.");
            }
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(StudentCreateDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating student with Unilogin {Unilogin}.", dto.Unilogin);

            try
            {
                var student = new Student
                {
                    Name = dto.Name,
                    Unilogin = dto.Unilogin,
                    DepartmentId = dto.DepartmentId,
                    StartDate = dto.StartDate,
                    GraduationDate = dto.GraduationDate,
                    MunicipalityId = dto.MunicipalityId,
                    Birthdate = dto.Birthdate,
                    SerialNumber = dto.SerialNumber,
                    IdFromUms = dto.IdFromUms,
                    UmsActivity = dto.UmsActivity
                };

                var result = await _studentService
                    .AddStudentAsync(student, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully created student with ID {StudentId}.", student.StudentId);
                    return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
                }
                else if (result == OperationResult.Failure)
                {
                    _logger.LogWarning("Failed to create student with Unilogin {Unilogin}.", dto.Unilogin);
                    return BadRequest("Failed to create the student.");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while creating student with ID {StudentId}.", student.StudentId);
                    return StatusCode(500, "An unexpected error occurred while creating the student.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while creating student with Unilogin {Unilogin}.", dto.Unilogin);
                return StatusCode(500, "An error occurred while creating the student.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating student with Unilogin {Unilogin}.", dto.Unilogin);
                return StatusCode(500, "An unexpected error occurred while creating the student.");
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting student with ID {StudentId}.", id);

            try
            {
                var result = await _studentService
                    .DeleteStudentAsync(id, cancellationToken);

                if (result == OperationResult.Success)
                {
                    _logger.LogInformation("Successfully deleted student with ID {StudentId}.", id);
                    return NoContent();
                }
                else if (result == OperationResult.NotFound)
                {
                    _logger.LogWarning("Student with ID {StudentId} not found.", id);
                    return NotFound($"Student with ID {id} not found.");
                }
                else
                {
                    _logger.LogError("Unexpected error occurred while deleting student with ID {StudentId}.", id);
                    return StatusCode(500, "An unexpected error occurred while deleting the student.");
                }
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "DataAccessException occurred while deleting student with ID {StudentId}.", id);
                return StatusCode(500, "An error occurred while deleting the student.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting student with ID {StudentId}.", id);
                return StatusCode(500, "An unexpected error occurred while deleting the student.");
            }
        }
    }
}
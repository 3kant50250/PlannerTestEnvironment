using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PlannerServer.Data;
using PlannerServer.Models;
using Microsoft.Extensions.Logging;
using PlannerServer.Repositories.Interfaces;
using PlannerServer.Services;
using Moq;
using PlannerServer.Enums;

namespace PlannerTests
{
    public class SchoolItemServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ISchoolItemRepository> _itemRepoMock;
        private readonly Mock<IStudentRepository> _studentRepoMock;
        private readonly Mock<ILogger<SchoolItem>> _loggerMock;
        private readonly SchoolItemService _service;

        public SchoolItemServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _itemRepoMock = new Mock<ISchoolItemRepository>();
            _studentRepoMock = new Mock<IStudentRepository>();
            _loggerMock = new Mock<ILogger<SchoolItem>>();

            _unitOfWorkMock.Setup(u => u.SchoolItemRepository).Returns(_itemRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.StudentRepository).Returns(_studentRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            _service = new SchoolItemService(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GivenAvailableItem_WhenRegisteredToStudent_ThenItemIsAssigned()
        {
            // Arrange
            var deptId = 1;
            var item = new SchoolItem { SchoolItemId = 1, DepartmentId = deptId };
            var student = new Student { StudentId = 2, DepartmentId = deptId, SchoolItems = new List<SchoolItem>() };

            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);
            _studentRepoMock.Setup(r => r.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(student);

            // Act
            var result = await _service.RegisterItemToStudent(1, 2, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Success);
            item.StudentId.Should().Be(2);
            student.SchoolItems.Should().Contain(item);
        }

        [Fact]
        public async Task GivenAssignedItem_WhenRegisteringToAnotherStudent_ThenFailureIsReturned()
        {
            // Arrange
            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((SchoolItem)null!);

            // Act
            var result = await _service.RegisterItemToStudent(1, 2, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Failure);
        }

        [Fact]
        public async Task GivenAssignedItem_WhenRegisteringToStudent_ThenFailureIsReturned()
        {
            // Arrange
            var item = new SchoolItem { SchoolItemId = 1, StudentId = 99, DepartmentId = 1 };
            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);

            // Act
            var result = await _service.RegisterItemToStudent(1, 2, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Failure);
        }

        [Fact]
        public async Task GivenStudentFromOtherDepartment_WhenRegisteringItem_ThenFailureIsReturned()
        {
            // Arrange
            var item = new SchoolItem { SchoolItemId = 1, DepartmentId = 1 };
            var student = new Student { StudentId = 2, DepartmentId = 2, SchoolItems = new List<SchoolItem>() };

            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);
            _studentRepoMock.Setup(r => r.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(student);

            // Act
            var result = await _service.RegisterItemToStudent(1, 2, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Failure);
        }

        [Fact]
        public async Task GivenAssignedItem_WhenDeregistering_ThenItemIsUnassigned()
        {
            // Arrange
            var item = new SchoolItem { SchoolItemId = 1, StudentId = 10, DepartmentId = 1 };
            var student = new Student { StudentId = 10, DepartmentId = 1, SchoolItems = new List<SchoolItem> { item } };

            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);
            _studentRepoMock.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>())).ReturnsAsync(student);
            _unitOfWorkMock.Setup(u => u.CompleteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _service.DeregisterItemFromStudentAsync(1, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Success);
            item.StudentId.Should().BeNull();
            student.SchoolItems.Should().NotContain(item);
        }

        [Fact]
        public async Task GivenUnassignedItem_WhenDeregistering_ThenFailureIsReturned()
        {
            // Arrange
            var item = new SchoolItem { SchoolItemId = 1, StudentId = null };

            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(item);

            // Act
            var result = await _service.DeregisterItemFromStudentAsync(1, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Failure);
        }

        [Fact]
        public async Task GivenNonexistentItem_WhenDeregistering_ThenFailureIsReturned()
        {
            // Arrange
            _itemRepoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((SchoolItem)null!);

            // Act
            var result = await _service.DeregisterItemFromStudentAsync(1, CancellationToken.None);

            // Assert
            result.Should().Be(OperationResult.Failure);
        }

    }
}

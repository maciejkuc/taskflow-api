using Ardalis.Result;
using FluentAssertions;
using NSubstitute;
using TaskFlow.Application.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Tests.Tasks;

public class CompleteTaskCommandHandlerTests
{
    private readonly ITaskItemRepository _taskItemRepository = Substitute.For<ITaskItemRepository>();
    private readonly CompleteTaskCommandHandler _sut;

    public CompleteTaskCommandHandlerTests() => _sut = new CompleteTaskCommandHandler(_taskItemRepository);

    [Fact]
    public async Task Handle_WhenTaskExists_ShouldReturnSuccess()
    {
        // Arrange
        var command = new CompleteTaskCommand(1);
        var taskItem = new TaskItem("My Task", 1);
        _taskItemRepository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(taskItem);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenTaskDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new CompleteTaskCommand(99);
        _taskItemRepository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((TaskItem?)null);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task Handle_WhenTaskExists_ShouldMarkTaskAsCompleted()
    {
        // Arrange
        var command = new CompleteTaskCommand(1);
        var taskItem = new TaskItem("My Task", 1);
        _taskItemRepository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(taskItem);

        // Act
        await _sut.Handle(command, CancellationToken.None);

        // Assert
        taskItem.IsCompleted.Should().BeTrue();
    }
}

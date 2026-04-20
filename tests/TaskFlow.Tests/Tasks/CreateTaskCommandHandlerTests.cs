using Ardalis.Result;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core;
using TaskFlow.Application.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Tests.Tasks;

public class CreateTaskCommandHandlerTests
{
    private readonly IProjectRepository _projectRepository = Substitute.For<IProjectRepository>();
    private readonly ITaskItemRepository _taskItemRepository = Substitute.For<ITaskItemRepository>();
    private readonly ILogger<CreateTaskCommandHandler> _logger = Substitute.For<ILogger<CreateTaskCommandHandler>>();
    private readonly CreateTaskCommandHandler _sut;

    public CreateTaskCommandHandlerTests() =>
        _sut = new CreateTaskCommandHandler(_projectRepository, _taskItemRepository, _logger);

    [Fact]
    public async Task Handle_WhenProjectExistsAndTitleValid_ShouldReturnSuccessWithTaskId()
    {
        // Arrange
        var command = new CreateTaskCommand(1, "My Task");
        var project = new Project("Test Project", null);
        var createdTask = new TaskItem("My Task", 1);
        _projectRepository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(project);
        _taskItemRepository.AddAsync(Arg.Any<TaskItem>(), Arg.Any<CancellationToken>()).Returns(createdTask);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(createdTask.Id);
    }

    [Fact]
    public async Task Handle_WhenProjectDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new CreateTaskCommand(99, "My Task");
        _projectRepository.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((Project?)null);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task Handle_WhenTaskCreatedSuccessfully_ShouldLogInformation()
    {
        // Arrange
        var command = new CreateTaskCommand(1, "My Task");
        var project = new Project("Test Project", null);
        var createdTask = new TaskItem("My Task", 1);
        _projectRepository.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(project);
        _taskItemRepository.AddAsync(Arg.Any<TaskItem>(), Arg.Any<CancellationToken>()).Returns(createdTask);

        // Act
        await _sut.Handle(command, CancellationToken.None);

        // Assert
        _logger.ReceivedCalls()
            .Should().ContainSingle(c =>
                c.GetMethodInfo().Name == "Log" &&
                LogLevel.Information.Equals(c.GetArguments()[0]));
    }
}

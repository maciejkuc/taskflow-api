using Ardalis.Result;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TaskFlow.Application.Projects;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Tests.Projects;

public class CreateProjectCommandHandlerTests
{
    private readonly IProjectRepository _projectRepository = Substitute.For<IProjectRepository>();
    private readonly ILogger<CreateProjectCommandHandler> _logger = Substitute.For<ILogger<CreateProjectCommandHandler>>();
    private readonly CreateProjectCommandHandler _sut;

    public CreateProjectCommandHandlerTests() => _sut = new CreateProjectCommandHandler(_projectRepository, _logger);

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldReturnSuccessWithProjectId()
    {
        // Arrange
        var command = new CreateProjectCommand("My Project", "A description");
        var createdProject = new Project("My Project", "A description");
        _projectRepository
            .AddAsync(Arg.Any<Project>(), Arg.Any<CancellationToken>())
            .Returns(createdProject);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(createdProject.Id);
    }

    [Fact]
    public async Task Handle_WhenProjectHasNoDescription_ShouldReturnSuccess()
    {
        // Arrange
        var command = new CreateProjectCommand("My Project", null);
        var createdProject = new Project("My Project", null);
        _projectRepository
            .AddAsync(Arg.Any<Project>(), Arg.Any<CancellationToken>())
            .Returns(createdProject);

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}

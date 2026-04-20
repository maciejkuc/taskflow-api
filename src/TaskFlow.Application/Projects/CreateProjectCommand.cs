using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Projects;

public record CreateProjectCommand(string Name, string? Description) : IRequest<Result<int>>;

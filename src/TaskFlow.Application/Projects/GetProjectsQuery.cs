using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Projects;

public record GetProjectsQuery : IRequest<Result<IEnumerable<ProjectDto>>>;

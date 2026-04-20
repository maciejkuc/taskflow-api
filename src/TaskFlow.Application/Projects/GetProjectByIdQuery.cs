using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Projects;

public record GetProjectByIdQuery(int Id) : IRequest<Result<ProjectDto>>;

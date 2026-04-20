using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Tasks;

public record GetTasksByProjectQuery(int ProjectId) : IRequest<Result<IEnumerable<TaskItemDto>>>;

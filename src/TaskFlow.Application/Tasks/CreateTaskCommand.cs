using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Tasks;

public record CreateTaskCommand(int ProjectId, string Title) : IRequest<Result<int>>;

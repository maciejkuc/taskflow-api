using Ardalis.Result;
using MediatR;

namespace TaskFlow.Application.Tasks;

public record CompleteTaskCommand(int TaskId) : IRequest<Result>;

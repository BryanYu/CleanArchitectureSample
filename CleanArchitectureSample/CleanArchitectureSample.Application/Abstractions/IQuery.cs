using MediatR;

namespace CleanArchitectureSample.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
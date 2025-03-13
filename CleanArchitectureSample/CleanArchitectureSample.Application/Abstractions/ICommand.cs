using MediatR;

namespace CleanArchitectureSample.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}
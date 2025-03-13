using CleanArchitectureSample.Application.Abstractions;
using FluentValidation;
using MediatR;
using ValidationException = CleanArchitectureSample.Application.Exceptions.ValidationException;

namespace CleanArchitectureSample.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : class, ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var errorsDictionary = _validators.Select(item => item.Validate(context))
            .SelectMany(item => item.Errors)
            .Where(item => item != null)
            .GroupBy(item => item.PropertyName, x => x.ErrorMessage,
                (propertyName, errorMessags) => new
                {
                    Key = propertyName,
                    Values = errorMessags.Distinct().ToArray()
                })
            .ToDictionary(item => item.Key, item => item.Values);

        if (errorsDictionary.Any())
        {
            throw new ValidationException(errorsDictionary);
        }

        return await next();

    }
}
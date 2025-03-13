using CleanArchitectureSample.Domain.Exceptions.Base;

namespace CleanArchitectureSample.Domain.Exceptions;

public sealed class WebinarNotFoundException : NotFoundException
{
    public WebinarNotFoundException(Guid webinarId)
        : base($"The webinar with the identifier {webinarId} was not found.")
    {
    }
}
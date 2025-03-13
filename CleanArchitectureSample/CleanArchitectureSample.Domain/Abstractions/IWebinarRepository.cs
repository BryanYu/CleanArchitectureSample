using CleanArchitectureSample.Domain.Entities;

namespace CleanArchitectureSample.Domain.Abstractions;

public interface IWebinarRepository
{
    void Insert(Webinar webinar);

    Task<Webinar?> GetWebinar(Guid webinarId);
}
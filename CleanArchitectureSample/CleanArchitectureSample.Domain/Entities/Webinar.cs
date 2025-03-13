using CleanArchitectureSample.Domain.Primitives;

namespace CleanArchitectureSample.Domain.Entities;

public class Webinar : Entity
{
    public Webinar()
    {
        
    }

    public Webinar(Guid id, string name, DateTime scheduledOn) : base(id)
    {
        Name = name;
        ScheduledOn = scheduledOn;
    }

    public string Name { get; private set; }

    public DateTime ScheduledOn { get; private set; }
}
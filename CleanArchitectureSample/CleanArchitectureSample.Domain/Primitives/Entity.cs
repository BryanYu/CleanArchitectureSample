namespace CleanArchitectureSample.Domain.Primitives;

public abstract class Entity
{
    protected Entity(Guid id) => id = id;

    protected Entity()
    {
        
    }
    
    public Guid Id { get; protected set; }
}
namespace CommitMaster.Hera.Core.Entities.Base;


public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}


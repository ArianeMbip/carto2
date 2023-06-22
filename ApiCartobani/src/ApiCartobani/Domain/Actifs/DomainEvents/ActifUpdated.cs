namespace ApiCartobani.Domain.Actifs.DomainEvents;

public sealed class ActifUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
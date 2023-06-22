namespace ApiCartobani.Domain.DAs.DomainEvents;

public sealed class DAUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
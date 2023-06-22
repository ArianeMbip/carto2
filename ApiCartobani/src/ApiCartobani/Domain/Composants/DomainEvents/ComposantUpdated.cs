namespace ApiCartobani.Domain.Composants.DomainEvents;

public sealed class ComposantUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
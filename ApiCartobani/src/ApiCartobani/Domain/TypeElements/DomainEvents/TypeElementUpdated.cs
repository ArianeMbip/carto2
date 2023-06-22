namespace ApiCartobani.Domain.TypeElements.DomainEvents;

public sealed class TypeElementUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
namespace ApiCartobani.Domain.TypeElements.DomainEvents;

public sealed class TypeElementCreated : DomainEvent
{
    public TypeElement TypeElement { get; set; } 
}
            
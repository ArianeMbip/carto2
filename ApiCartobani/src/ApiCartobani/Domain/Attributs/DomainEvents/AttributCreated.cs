namespace ApiCartobani.Domain.Attributs.DomainEvents;

public sealed class AttributCreated : DomainEvent
{
    public Attribut Attribut { get; set; } 
}
            
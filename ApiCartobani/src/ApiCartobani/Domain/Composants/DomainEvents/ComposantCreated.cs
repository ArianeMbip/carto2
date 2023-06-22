namespace ApiCartobani.Domain.Composants.DomainEvents;

public sealed class ComposantCreated : DomainEvent
{
    public Composant Composant { get; set; } 
}
            
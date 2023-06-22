namespace ApiCartobani.Domain.Fonctionnalites.DomainEvents;

public sealed class FonctionnaliteUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
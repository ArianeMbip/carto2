namespace ApiCartobani.Domain.Fonctionnalites.DomainEvents;

public sealed class FonctionnaliteCreated : DomainEvent
{
    public Fonctionnalite Fonctionnalite { get; set; } 
}
            
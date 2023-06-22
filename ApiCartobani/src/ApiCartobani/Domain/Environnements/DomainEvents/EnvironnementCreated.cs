namespace ApiCartobani.Domain.Environnements.DomainEvents;

public sealed class EnvironnementCreated : DomainEvent
{
    public Environnement Environnement { get; set; } 
}
            
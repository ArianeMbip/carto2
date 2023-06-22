namespace ApiCartobani.Domain.Environnements.DomainEvents;

public sealed class EnvironnementUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
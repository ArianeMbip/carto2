namespace ApiCartobani.Domain.Actifs.DomainEvents;

public sealed class ActifCreated : DomainEvent
{
    public Actif Actif { get; set; } 
}
            
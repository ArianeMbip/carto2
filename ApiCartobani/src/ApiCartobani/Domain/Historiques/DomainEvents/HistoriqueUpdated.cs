namespace ApiCartobani.Domain.Historiques.DomainEvents;

public sealed class HistoriqueUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
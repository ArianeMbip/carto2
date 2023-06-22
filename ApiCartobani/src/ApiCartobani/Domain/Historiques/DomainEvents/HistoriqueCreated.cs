namespace ApiCartobani.Domain.Historiques.DomainEvents;

public sealed class HistoriqueCreated : DomainEvent
{
    public Historique Historique { get; set; } 
}
            
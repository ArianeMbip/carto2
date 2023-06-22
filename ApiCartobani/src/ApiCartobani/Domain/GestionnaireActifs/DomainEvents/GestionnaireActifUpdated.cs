namespace ApiCartobani.Domain.GestionnaireActifs.DomainEvents;

public sealed class GestionnaireActifUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
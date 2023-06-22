namespace ApiCartobani.Domain.GestionnaireActifs.DomainEvents;

public sealed class GestionnaireActifCreated : DomainEvent
{
    public GestionnaireActif GestionnaireActif { get; set; } 
}
            
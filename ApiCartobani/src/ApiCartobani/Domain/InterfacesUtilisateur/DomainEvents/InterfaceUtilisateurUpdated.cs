namespace ApiCartobani.Domain.InterfacesUtilisateur.DomainEvents;

public sealed class InterfaceUtilisateurUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
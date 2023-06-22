namespace ApiCartobani.Domain.InterfacesUtilisateur.DomainEvents;

public sealed class InterfaceUtilisateurCreated : DomainEvent
{
    public InterfaceUtilisateur InterfaceUtilisateur { get; set; } 
}
            
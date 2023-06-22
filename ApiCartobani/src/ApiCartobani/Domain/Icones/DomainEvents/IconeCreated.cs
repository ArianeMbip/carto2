namespace ApiCartobani.Domain.Icones.DomainEvents;

public sealed class IconeCreated : DomainEvent
{
    public Icone Icone { get; set; } 
}
            
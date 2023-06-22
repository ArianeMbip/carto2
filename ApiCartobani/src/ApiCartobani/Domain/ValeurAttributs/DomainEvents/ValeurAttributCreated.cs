namespace ApiCartobani.Domain.ValeurAttributs.DomainEvents;

public sealed class ValeurAttributCreated : DomainEvent
{
    public ValeurAttribut ValeurAttribut { get; set; } 
}
            
namespace ApiCartobani.Domain.DAs.DomainEvents;

public sealed class DACreated : DomainEvent
{
    public DA DA { get; set; } 
}
            
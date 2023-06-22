namespace ApiCartobani.Domain.Universs.DomainEvents;

public sealed class UniversCreated : DomainEvent
{
    public Univers Univers { get; set; } 
}
            
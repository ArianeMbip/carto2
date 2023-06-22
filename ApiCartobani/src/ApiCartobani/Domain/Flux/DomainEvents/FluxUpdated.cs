namespace ApiCartobani.Domain.Flux.DomainEvents;

public sealed class FluxUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
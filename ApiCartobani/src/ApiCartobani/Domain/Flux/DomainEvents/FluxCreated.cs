namespace ApiCartobani.Domain.Flux.DomainEvents;

public sealed class FluxCreated : DomainEvent
{
    public Flux Flux { get; set; } 
}
            
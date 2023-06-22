namespace ApiCartobani.Domain.PiecesJointes.DomainEvents;

public sealed class PieceJointeUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            
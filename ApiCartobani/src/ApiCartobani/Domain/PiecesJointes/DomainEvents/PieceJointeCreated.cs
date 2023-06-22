namespace ApiCartobani.Domain.PiecesJointes.DomainEvents;

public sealed class PieceJointeCreated : DomainEvent
{
    public PieceJointe PieceJointe { get; set; } 
}
            
namespace ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;

using AutoBogus;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;

public sealed class FakePieceJointe
{
    public static PieceJointe Generate(PieceJointeForCreationDto pieceJointeForCreationDto)
    {
        return PieceJointe.Create(pieceJointeForCreationDto);
    }

    public static PieceJointe Generate()
    {
        return Generate(new FakePieceJointeForCreationDto().Generate());
    }
}
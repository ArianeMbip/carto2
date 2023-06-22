namespace ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;

using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;

public class FakePieceJointeBuilder
{
    private PieceJointeForCreationDto _creationData = new FakePieceJointeForCreationDto().Generate();

    public FakePieceJointeBuilder WithDto(PieceJointeForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public PieceJointe Build()
    {
        var result = PieceJointe.Create(_creationData);
        return result;
    }
}
namespace ApiCartobani.Domain.PiecesJointes.Validators;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using FluentValidation;

public sealed class PieceJointeForCreationDtoValidator: PieceJointeForManipulationDtoValidator<PieceJointeForCreationDto>
{
    public PieceJointeForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
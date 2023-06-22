namespace ApiCartobani.Domain.PiecesJointes.Validators;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using FluentValidation;

public sealed class PieceJointeForUpdateDtoValidator: PieceJointeForManipulationDtoValidator<PieceJointeForUpdateDto>
{
    public PieceJointeForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
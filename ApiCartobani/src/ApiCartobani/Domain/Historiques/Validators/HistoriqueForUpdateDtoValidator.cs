namespace ApiCartobani.Domain.Historiques.Validators;

using ApiCartobani.Domain.Historiques.Dtos;
using FluentValidation;

public sealed class HistoriqueForUpdateDtoValidator: HistoriqueForManipulationDtoValidator<HistoriqueForUpdateDto>
{
    public HistoriqueForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
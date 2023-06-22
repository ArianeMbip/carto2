namespace ApiCartobani.Domain.Historiques.Validators;

using ApiCartobani.Domain.Historiques.Dtos;
using FluentValidation;

public sealed class HistoriqueForCreationDtoValidator: HistoriqueForManipulationDtoValidator<HistoriqueForCreationDto>
{
    public HistoriqueForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
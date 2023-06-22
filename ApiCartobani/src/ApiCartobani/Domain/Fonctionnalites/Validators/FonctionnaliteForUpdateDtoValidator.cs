namespace ApiCartobani.Domain.Fonctionnalites.Validators;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using FluentValidation;

public sealed class FonctionnaliteForUpdateDtoValidator: FonctionnaliteForManipulationDtoValidator<FonctionnaliteForUpdateDto>
{
    public FonctionnaliteForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
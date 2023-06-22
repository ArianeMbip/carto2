namespace ApiCartobani.Domain.Fonctionnalites.Validators;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using FluentValidation;

public sealed class FonctionnaliteForCreationDtoValidator: FonctionnaliteForManipulationDtoValidator<FonctionnaliteForCreationDto>
{
    public FonctionnaliteForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
namespace ApiCartobani.Domain.Environnements.Validators;

using ApiCartobani.Domain.Environnements.Dtos;
using FluentValidation;

public sealed class EnvironnementForCreationDtoValidator: EnvironnementForManipulationDtoValidator<EnvironnementForCreationDto>
{
    public EnvironnementForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
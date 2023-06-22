namespace ApiCartobani.Domain.Environnements.Validators;

using ApiCartobani.Domain.Environnements.Dtos;
using FluentValidation;

public sealed class EnvironnementForUpdateDtoValidator: EnvironnementForManipulationDtoValidator<EnvironnementForUpdateDto>
{
    public EnvironnementForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
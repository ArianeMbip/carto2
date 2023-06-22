namespace ApiCartobani.Domain.ValeurAttributs.Validators;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using FluentValidation;

public sealed class ValeurAttributForUpdateDtoValidator: ValeurAttributForManipulationDtoValidator<ValeurAttributForUpdateDto>
{
    public ValeurAttributForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
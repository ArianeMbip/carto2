namespace ApiCartobani.Domain.ValeurAttributs.Validators;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using FluentValidation;

public sealed class ValeurAttributForCreationDtoValidator: ValeurAttributForManipulationDtoValidator<ValeurAttributForCreationDto>
{
    public ValeurAttributForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
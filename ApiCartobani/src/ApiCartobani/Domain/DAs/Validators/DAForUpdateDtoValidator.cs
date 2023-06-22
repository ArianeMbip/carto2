namespace ApiCartobani.Domain.DAs.Validators;

using ApiCartobani.Domain.DAs.Dtos;
using FluentValidation;

public sealed class DAForUpdateDtoValidator: DAForManipulationDtoValidator<DAForUpdateDto>
{
    public DAForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
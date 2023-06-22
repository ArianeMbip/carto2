namespace ApiCartobani.Domain.DAs.Validators;

using ApiCartobani.Domain.DAs.Dtos;
using FluentValidation;

public sealed class DAForCreationDtoValidator: DAForManipulationDtoValidator<DAForCreationDto>
{
    public DAForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
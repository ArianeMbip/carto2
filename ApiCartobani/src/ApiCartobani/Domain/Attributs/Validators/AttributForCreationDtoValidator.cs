namespace ApiCartobani.Domain.Attributs.Validators;

using ApiCartobani.Domain.Attributs.Dtos;
using FluentValidation;

public sealed class AttributForCreationDtoValidator: AttributForManipulationDtoValidator<AttributForCreationDto>
{
    public AttributForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
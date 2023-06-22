namespace ApiCartobani.Domain.Attributs.Validators;

using ApiCartobani.Domain.Attributs.Dtos;
using FluentValidation;

public sealed class AttributForUpdateDtoValidator: AttributForManipulationDtoValidator<AttributForUpdateDto>
{
    public AttributForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
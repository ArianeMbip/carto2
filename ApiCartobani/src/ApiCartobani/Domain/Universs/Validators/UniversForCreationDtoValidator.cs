namespace ApiCartobani.Domain.Universs.Validators;

using ApiCartobani.Domain.Universs.Dtos;
using FluentValidation;

public sealed class UniversForCreationDtoValidator: UniversForManipulationDtoValidator<UniversForCreationDto>
{
    public UniversForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
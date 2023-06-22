namespace ApiCartobani.Domain.Universs.Validators;

using ApiCartobani.Domain.Universs.Dtos;
using FluentValidation;

public sealed class UniversForUpdateDtoValidator: UniversForManipulationDtoValidator<UniversForUpdateDto>
{
    public UniversForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
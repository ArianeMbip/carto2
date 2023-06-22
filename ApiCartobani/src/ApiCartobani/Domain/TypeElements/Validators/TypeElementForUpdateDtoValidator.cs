namespace ApiCartobani.Domain.TypeElements.Validators;

using ApiCartobani.Domain.TypeElements.Dtos;
using FluentValidation;

public sealed class TypeElementForUpdateDtoValidator: TypeElementForManipulationDtoValidator<TypeElementForUpdateDto>
{
    public TypeElementForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
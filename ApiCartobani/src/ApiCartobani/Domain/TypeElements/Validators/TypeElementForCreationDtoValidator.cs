namespace ApiCartobani.Domain.TypeElements.Validators;

using ApiCartobani.Domain.TypeElements.Dtos;
using FluentValidation;

public sealed class TypeElementForCreationDtoValidator: TypeElementForManipulationDtoValidator<TypeElementForCreationDto>
{
    public TypeElementForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
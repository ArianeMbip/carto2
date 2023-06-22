namespace ApiCartobani.Domain.Icones.Validators;

using ApiCartobani.Domain.Icones.Dtos;
using FluentValidation;

public sealed class IconeForCreationDtoValidator: IconeForManipulationDtoValidator<IconeForCreationDto>
{
    public IconeForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
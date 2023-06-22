namespace ApiCartobani.Domain.Icones.Validators;

using ApiCartobani.Domain.Icones.Dtos;
using FluentValidation;

public sealed class IconeForUpdateDtoValidator: IconeForManipulationDtoValidator<IconeForUpdateDto>
{
    public IconeForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
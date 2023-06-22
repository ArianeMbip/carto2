namespace ApiCartobani.Domain.Composants.Validators;

using ApiCartobani.Domain.Composants.Dtos;
using FluentValidation;

public sealed class ComposantForUpdateDtoValidator: ComposantForManipulationDtoValidator<ComposantForUpdateDto>
{
    public ComposantForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
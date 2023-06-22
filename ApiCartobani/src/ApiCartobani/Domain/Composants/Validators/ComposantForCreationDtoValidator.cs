namespace ApiCartobani.Domain.Composants.Validators;

using ApiCartobani.Domain.Composants.Dtos;
using FluentValidation;

public sealed class ComposantForCreationDtoValidator: ComposantForManipulationDtoValidator<ComposantForCreationDto>
{
    public ComposantForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
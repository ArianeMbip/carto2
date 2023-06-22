namespace ApiCartobani.Domain.Flux.Validators;

using ApiCartobani.Domain.Flux.Dtos;
using FluentValidation;

public sealed class FluxForCreationDtoValidator: FluxForManipulationDtoValidator<FluxForCreationDto>
{
    public FluxForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
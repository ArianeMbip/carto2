namespace ApiCartobani.Domain.Flux.Validators;

using ApiCartobani.Domain.Flux.Dtos;
using FluentValidation;

public sealed class FluxForUpdateDtoValidator: FluxForManipulationDtoValidator<FluxForUpdateDto>
{
    public FluxForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
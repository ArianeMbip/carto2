namespace ApiCartobani.Domain.Actifs.Validators;

using ApiCartobani.Domain.Actifs.Dtos;
using FluentValidation;

public sealed class ActifForCreationDtoValidator: ActifForManipulationDtoValidator<ActifForCreationDto>
{
    public ActifForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
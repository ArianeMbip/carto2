namespace ApiCartobani.Domain.Actifs.Validators;

using ApiCartobani.Domain.Actifs.Dtos;
using FluentValidation;

public sealed class ActifForUpdateDtoValidator: ActifForManipulationDtoValidator<ActifForUpdateDto>
{
    public ActifForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
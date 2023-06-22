namespace ApiCartobani.Domain.GestionnaireActifs.Validators;

using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using FluentValidation;

public sealed class GestionnaireActifForUpdateDtoValidator: GestionnaireActifForManipulationDtoValidator<GestionnaireActifForUpdateDto>
{
    public GestionnaireActifForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
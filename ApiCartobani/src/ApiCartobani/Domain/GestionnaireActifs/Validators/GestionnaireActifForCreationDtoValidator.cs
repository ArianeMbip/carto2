namespace ApiCartobani.Domain.GestionnaireActifs.Validators;

using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using FluentValidation;

public sealed class GestionnaireActifForCreationDtoValidator: GestionnaireActifForManipulationDtoValidator<GestionnaireActifForCreationDto>
{
    public GestionnaireActifForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
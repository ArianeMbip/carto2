namespace ApiCartobani.Domain.InterfacesUtilisateur.Validators;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using FluentValidation;

public sealed class InterfaceUtilisateurForUpdateDtoValidator: InterfaceUtilisateurForManipulationDtoValidator<InterfaceUtilisateurForUpdateDto>
{
    public InterfaceUtilisateurForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
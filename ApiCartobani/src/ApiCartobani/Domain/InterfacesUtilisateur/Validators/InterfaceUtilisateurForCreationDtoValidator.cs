namespace ApiCartobani.Domain.InterfacesUtilisateur.Validators;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using FluentValidation;

public sealed class InterfaceUtilisateurForCreationDtoValidator: InterfaceUtilisateurForManipulationDtoValidator<InterfaceUtilisateurForCreationDto>
{
    public InterfaceUtilisateurForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
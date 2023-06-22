namespace ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;

using AutoBogus;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;

public sealed class FakeInterfaceUtilisateur
{
    public static InterfaceUtilisateur Generate(InterfaceUtilisateurForCreationDto interfaceUtilisateurForCreationDto)
    {
        return InterfaceUtilisateur.Create(interfaceUtilisateurForCreationDto);
    }

    public static InterfaceUtilisateur Generate()
    {
        return Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
    }
}
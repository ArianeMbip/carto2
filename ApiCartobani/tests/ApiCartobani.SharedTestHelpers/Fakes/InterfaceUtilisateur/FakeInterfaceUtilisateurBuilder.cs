namespace ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;

using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;

public class FakeInterfaceUtilisateurBuilder
{
    private InterfaceUtilisateurForCreationDto _creationData = new FakeInterfaceUtilisateurForCreationDto().Generate();

    public FakeInterfaceUtilisateurBuilder WithDto(InterfaceUtilisateurForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public InterfaceUtilisateur Build()
    {
        var result = InterfaceUtilisateur.Create(_creationData);
        return result;
    }
}
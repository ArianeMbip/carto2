namespace ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;

using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Dtos;

public class FakeGestionnaireActifBuilder
{
    private GestionnaireActifForCreationDto _creationData = new FakeGestionnaireActifForCreationDto().Generate();

    public FakeGestionnaireActifBuilder WithDto(GestionnaireActifForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public GestionnaireActif Build()
    {
        var result = GestionnaireActif.Create(_creationData);
        return result;
    }
}
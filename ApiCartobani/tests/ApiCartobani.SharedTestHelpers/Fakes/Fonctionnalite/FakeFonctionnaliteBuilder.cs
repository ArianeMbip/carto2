namespace ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;

using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;

public class FakeFonctionnaliteBuilder
{
    private FonctionnaliteForCreationDto _creationData = new FakeFonctionnaliteForCreationDto().Generate();

    public FakeFonctionnaliteBuilder WithDto(FonctionnaliteForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Fonctionnalite Build()
    {
        var result = Fonctionnalite.Create(_creationData);
        return result;
    }
}
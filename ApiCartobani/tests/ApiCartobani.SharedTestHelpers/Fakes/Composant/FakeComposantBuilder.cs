namespace ApiCartobani.SharedTestHelpers.Fakes.Composant;

using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.Dtos;

public class FakeComposantBuilder
{
    private ComposantForCreationDto _creationData = new FakeComposantForCreationDto().Generate();

    public FakeComposantBuilder WithDto(ComposantForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Composant Build()
    {
        var result = Composant.Create(_creationData);
        return result;
    }
}
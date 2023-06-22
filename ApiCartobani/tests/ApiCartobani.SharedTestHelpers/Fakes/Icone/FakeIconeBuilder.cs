namespace ApiCartobani.SharedTestHelpers.Fakes.Icone;

using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;

public class FakeIconeBuilder
{
    private IconeForCreationDto _creationData = new FakeIconeForCreationDto().Generate();

    public FakeIconeBuilder WithDto(IconeForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Icone Build()
    {
        var result = Icone.Create(_creationData);
        return result;
    }
}
namespace ApiCartobani.SharedTestHelpers.Fakes.Attribut;

using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;

public class FakeAttributBuilder
{
    private AttributForCreationDto _creationData = new FakeAttributForCreationDto().Generate();

    public FakeAttributBuilder WithDto(AttributForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Attribut Build()
    {
        var result = Attribut.Create(_creationData);
        return result;
    }
}
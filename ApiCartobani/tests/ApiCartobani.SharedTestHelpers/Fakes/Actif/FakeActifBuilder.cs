namespace ApiCartobani.SharedTestHelpers.Fakes.Actif;

using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;

public class FakeActifBuilder
{
    private ActifForCreationDto _creationData = new FakeActifForCreationDto().Generate();

    public FakeActifBuilder WithDto(ActifForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Actif Build()
    {
        var result = Actif.Create(_creationData);
        return result;
    }
}
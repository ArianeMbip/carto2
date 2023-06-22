namespace ApiCartobani.SharedTestHelpers.Fakes.Univers;

using ApiCartobani.Domain.Univers;
using ApiCartobani.Domain.Univers.Dtos;

public class FakeUniversBuilder
{
    private UniversForCreationDto _creationData = new FakeUniversForCreationDto().Generate();

    public FakeUniversBuilder WithDto(UniversForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Univers Build()
    {
        var result = Univers.Create(_creationData);
        return result;
    }
}
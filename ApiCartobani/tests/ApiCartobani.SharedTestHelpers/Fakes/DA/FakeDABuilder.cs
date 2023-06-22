namespace ApiCartobani.SharedTestHelpers.Fakes.DA;

using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;

public class FakeDABuilder
{
    private DAForCreationDto _creationData = new FakeDAForCreationDto().Generate();

    public FakeDABuilder WithDto(DAForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public DA Build()
    {
        var result = DA.Create(_creationData);
        return result;
    }
}
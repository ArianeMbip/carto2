namespace ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;

using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;

public class FakeValeurAttributBuilder
{
    private ValeurAttributForCreationDto _creationData = new FakeValeurAttributForCreationDto().Generate();

    public FakeValeurAttributBuilder WithDto(ValeurAttributForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public ValeurAttribut Build()
    {
        var result = ValeurAttribut.Create(_creationData);
        return result;
    }
}
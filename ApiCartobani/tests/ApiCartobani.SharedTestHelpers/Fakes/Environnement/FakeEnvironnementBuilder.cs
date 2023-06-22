namespace ApiCartobani.SharedTestHelpers.Fakes.Environnement;

using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.Dtos;

public class FakeEnvironnementBuilder
{
    private EnvironnementForCreationDto _creationData = new FakeEnvironnementForCreationDto().Generate();

    public FakeEnvironnementBuilder WithDto(EnvironnementForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Environnement Build()
    {
        var result = Environnement.Create(_creationData);
        return result;
    }
}
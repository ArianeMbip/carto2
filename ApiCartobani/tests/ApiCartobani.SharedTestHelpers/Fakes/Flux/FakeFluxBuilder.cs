namespace ApiCartobani.SharedTestHelpers.Fakes.Flux;

using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;

public class FakeFluxBuilder
{
    private FluxForCreationDto _creationData = new FakeFluxForCreationDto().Generate();

    public FakeFluxBuilder WithDto(FluxForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Flux Build()
    {
        var result = Flux.Create(_creationData);
        return result;
    }
}
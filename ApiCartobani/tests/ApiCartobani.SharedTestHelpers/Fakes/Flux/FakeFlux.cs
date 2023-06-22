namespace ApiCartobani.SharedTestHelpers.Fakes.Flux;

using AutoBogus;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;

public sealed class FakeFlux
{
    public static Flux Generate(FluxForCreationDto fluxForCreationDto)
    {
        return Flux.Create(fluxForCreationDto);
    }

    public static Flux Generate()
    {
        return Generate(new FakeFluxForCreationDto().Generate());
    }
}
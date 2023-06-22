namespace ApiCartobani.Domain.Flux.Mappings;

using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux;
using Mapster;

public sealed class FluxMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Flux, FluxDto>();
        config.NewConfig<FluxForCreationDto, Flux>()
            .TwoWays();
        config.NewConfig<FluxForUpdateDto, Flux>()
            .TwoWays();
    }
}
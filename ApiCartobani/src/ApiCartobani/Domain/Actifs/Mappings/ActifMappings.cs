namespace ApiCartobani.Domain.Actifs.Mappings;

using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs;
using Mapster;

public sealed class ActifMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Actif, ActifDto>();
        config.NewConfig<ActifForCreationDto, Actif>()
            .TwoWays();
        config.NewConfig<ActifForUpdateDto, Actif>()
            .TwoWays();
    }
}
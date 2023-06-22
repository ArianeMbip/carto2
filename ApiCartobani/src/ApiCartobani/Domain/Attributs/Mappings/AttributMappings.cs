namespace ApiCartobani.Domain.Attributs.Mappings;

using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs;
using Mapster;

public sealed class AttributMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Attribut, AttributDto>();
        config.NewConfig<AttributForCreationDto, Attribut>()
            .TwoWays();
        config.NewConfig<AttributForUpdateDto, Attribut>()
            .TwoWays();
    }
}
namespace ApiCartobani.Domain.Universs.Mappings;

using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Domain.Universs;
using Mapster;

public sealed class UniversMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Univers, UniversDto>();
        config.NewConfig<UniversForCreationDto, Univers>()
            .TwoWays();
        config.NewConfig<UniversForUpdateDto, Univers>()
            .TwoWays();
    }
}
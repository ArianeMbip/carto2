namespace ApiCartobani.Domain.Icones.Mappings;

using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones;
using Mapster;

public sealed class IconeMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Icone, IconeDto>();
        config.NewConfig<IconeForCreationDto, Icone>()
            .TwoWays();
        config.NewConfig<IconeForUpdateDto, Icone>()
            .TwoWays();
    }
}
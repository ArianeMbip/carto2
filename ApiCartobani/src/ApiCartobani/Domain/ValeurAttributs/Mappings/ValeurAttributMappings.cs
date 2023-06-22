namespace ApiCartobani.Domain.ValeurAttributs.Mappings;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs;
using Mapster;

public sealed class ValeurAttributMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ValeurAttribut, ValeurAttributDto>();
        config.NewConfig<ValeurAttributForCreationDto, ValeurAttribut>()
            .TwoWays();
        config.NewConfig<ValeurAttributForUpdateDto, ValeurAttribut>()
            .TwoWays();
    }
}
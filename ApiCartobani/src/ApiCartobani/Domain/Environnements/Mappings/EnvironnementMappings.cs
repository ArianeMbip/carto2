namespace ApiCartobani.Domain.Environnements.Mappings;

using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements;
using Mapster;

public sealed class EnvironnementMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Environnement, EnvironnementDto>();
        config.NewConfig<EnvironnementForCreationDto, Environnement>()
            .TwoWays();
        config.NewConfig<EnvironnementForUpdateDto, Environnement>()
            .TwoWays();
    }
}
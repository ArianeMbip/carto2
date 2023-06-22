namespace ApiCartobani.Domain.Composants.Mappings;

using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants;
using Mapster;

public sealed class ComposantMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Composant, ComposantDto>();
        config.NewConfig<ComposantForCreationDto, Composant>()
            .TwoWays();
        config.NewConfig<ComposantForUpdateDto, Composant>()
            .TwoWays();
    }
}
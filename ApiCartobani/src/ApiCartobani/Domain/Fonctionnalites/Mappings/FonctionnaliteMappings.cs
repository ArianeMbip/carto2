namespace ApiCartobani.Domain.Fonctionnalites.Mappings;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites;
using Mapster;

public sealed class FonctionnaliteMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Fonctionnalite, FonctionnaliteDto>();
        config.NewConfig<FonctionnaliteForCreationDto, Fonctionnalite>()
            .TwoWays();
        config.NewConfig<FonctionnaliteForUpdateDto, Fonctionnalite>()
            .TwoWays();
    }
}
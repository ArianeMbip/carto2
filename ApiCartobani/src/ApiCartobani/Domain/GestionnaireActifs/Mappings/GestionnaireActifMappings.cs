namespace ApiCartobani.Domain.GestionnaireActifs.Mappings;

using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs;
using Mapster;

public sealed class GestionnaireActifMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GestionnaireActif, GestionnaireActifDto>();
        config.NewConfig<GestionnaireActifForCreationDto, GestionnaireActif>()
            .TwoWays();
        config.NewConfig<GestionnaireActifForUpdateDto, GestionnaireActif>()
            .TwoWays();
    }
}
namespace ApiCartobani.Domain.Historiques.Mappings;

using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques;
using Mapster;

public sealed class HistoriqueMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Historique, HistoriqueDto>();
        config.NewConfig<HistoriqueForCreationDto, Historique>()
            .TwoWays();
        config.NewConfig<HistoriqueForUpdateDto, Historique>()
            .TwoWays();
    }
}
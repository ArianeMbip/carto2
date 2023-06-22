namespace ApiCartobani.Domain.DAs.Mappings;

using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs;
using Mapster;

public sealed class DAMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DA, DADto>();
        config.NewConfig<DAForCreationDto, DA>()
            .TwoWays();
        config.NewConfig<DAForUpdateDto, DA>()
            .TwoWays();
    }
}
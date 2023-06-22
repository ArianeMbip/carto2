namespace ApiCartobani.Domain.TypeElements.Mappings;

using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Domain.TypeElements;
using Mapster;

public sealed class TypeElementMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TypeElement, TypeElementDto>();
        config.NewConfig<TypeElementForCreationDto, TypeElement>()
            .TwoWays();
        config.NewConfig<TypeElementForUpdateDto, TypeElement>()
            .TwoWays();
    }
}
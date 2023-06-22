namespace ApiCartobani.Domain.InterfacesUtilisateur.Mappings;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur;
using Mapster;

public sealed class InterfaceUtilisateurMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<InterfaceUtilisateur, InterfaceUtilisateurDto>();
        config.NewConfig<InterfaceUtilisateurForCreationDto, InterfaceUtilisateur>()
            .TwoWays();
        config.NewConfig<InterfaceUtilisateurForUpdateDto, InterfaceUtilisateur>()
            .TwoWays();
    }
}
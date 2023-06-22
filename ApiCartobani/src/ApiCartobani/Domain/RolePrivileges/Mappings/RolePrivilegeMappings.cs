namespace ApiCartobani.Domain.RolePrivileges.Mappings;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges;
using Mapster;

public sealed class RolePrivilegeMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RolePrivilege, RolePrivilegeDto>();
        config.NewConfig<RolePrivilegeForCreationDto, RolePrivilege>()
            .TwoWays();
        config.NewConfig<RolePrivilegeForUpdateDto, RolePrivilege>()
            .TwoWays();
    }
}
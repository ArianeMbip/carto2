namespace ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;

using AutoBogus;
using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.Dtos;

public sealed class FakeRolePrivilege
{
    public static RolePrivilege Generate(RolePrivilegeForCreationDto rolePrivilegeForCreationDto)
    {
        return RolePrivilege.Create(rolePrivilegeForCreationDto);
    }

    public static RolePrivilege Generate()
    {
        return Generate(new FakeRolePrivilegeForCreationDto().Generate());
    }
}
namespace ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;

using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.Dtos;

public class FakeRolePrivilegeBuilder
{
    private RolePrivilegeForCreationDto _creationData = new FakeRolePrivilegeForCreationDto().Generate();

    public FakeRolePrivilegeBuilder WithDto(RolePrivilegeForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public RolePrivilege Build()
    {
        var result = RolePrivilege.Create(_creationData);
        return result;
    }
}
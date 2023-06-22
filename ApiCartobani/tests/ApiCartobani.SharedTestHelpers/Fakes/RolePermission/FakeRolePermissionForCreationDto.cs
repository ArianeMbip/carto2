namespace ApiCartobani.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using ApiCartobani.Domain;
using ApiCartobani.Domain.RolePermissions.Dtos;
using ApiCartobani.Domain.Roles;

public sealed class FakeRolePermissionForCreationDto : AutoFaker<RolePermissionForCreationDto>
{
    public FakeRolePermissionForCreationDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Role.ListNames()));
    }
}
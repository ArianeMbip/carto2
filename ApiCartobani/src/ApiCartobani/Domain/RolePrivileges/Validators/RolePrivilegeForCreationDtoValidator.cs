namespace ApiCartobani.Domain.RolePrivileges.Validators;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using FluentValidation;

public sealed class RolePrivilegeForCreationDtoValidator: RolePrivilegeForManipulationDtoValidator<RolePrivilegeForCreationDto>
{
    public RolePrivilegeForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
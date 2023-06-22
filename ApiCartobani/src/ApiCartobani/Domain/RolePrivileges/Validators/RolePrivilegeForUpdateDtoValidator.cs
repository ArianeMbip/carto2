namespace ApiCartobani.Domain.RolePrivileges.Validators;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using FluentValidation;

public sealed class RolePrivilegeForUpdateDtoValidator: RolePrivilegeForManipulationDtoValidator<RolePrivilegeForUpdateDto>
{
    public RolePrivilegeForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
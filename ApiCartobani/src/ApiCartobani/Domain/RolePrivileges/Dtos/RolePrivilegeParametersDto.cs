namespace ApiCartobani.Domain.RolePrivileges.Dtos;

using SharedKernel.Dtos;

public sealed class RolePrivilegeParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

namespace ApiCartobani.Domain.ValeurAttributs.Dtos;

using SharedKernel.Dtos;

public sealed class ValeurAttributParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

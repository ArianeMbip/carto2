namespace ApiCartobani.Domain.Universs.Dtos;

using SharedKernel.Dtos;

public sealed class UniversParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

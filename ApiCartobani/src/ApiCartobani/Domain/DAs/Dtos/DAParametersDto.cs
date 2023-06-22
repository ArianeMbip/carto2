namespace ApiCartobani.Domain.DAs.Dtos;

using SharedKernel.Dtos;

public sealed class DAParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

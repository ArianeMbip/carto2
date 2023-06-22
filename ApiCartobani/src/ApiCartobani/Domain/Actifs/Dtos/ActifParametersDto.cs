namespace ApiCartobani.Domain.Actifs.Dtos;

using SharedKernel.Dtos;

public sealed class ActifParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

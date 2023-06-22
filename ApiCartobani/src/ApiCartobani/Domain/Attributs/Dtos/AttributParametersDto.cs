namespace ApiCartobani.Domain.Attributs.Dtos;

using SharedKernel.Dtos;

public sealed class AttributParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

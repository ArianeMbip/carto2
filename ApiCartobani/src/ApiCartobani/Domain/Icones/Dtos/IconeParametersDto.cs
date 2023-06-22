namespace ApiCartobani.Domain.Icones.Dtos;

using SharedKernel.Dtos;

public sealed class IconeParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

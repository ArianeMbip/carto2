namespace ApiCartobani.Domain.Historiques.Dtos;

using SharedKernel.Dtos;

public sealed class HistoriqueParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

namespace ApiCartobani.Domain.Composants.Dtos;

using SharedKernel.Dtos;

public sealed class ComposantParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

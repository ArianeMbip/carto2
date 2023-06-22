namespace ApiCartobani.Domain.Flux.Dtos;

using SharedKernel.Dtos;

public sealed class FluxParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

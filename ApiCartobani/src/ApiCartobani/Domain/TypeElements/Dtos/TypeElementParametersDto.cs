namespace ApiCartobani.Domain.TypeElements.Dtos;

using SharedKernel.Dtos;

public sealed class TypeElementParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

namespace ApiCartobani.Domain.Environnements.Dtos;

using SharedKernel.Dtos;

public sealed class EnvironnementParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

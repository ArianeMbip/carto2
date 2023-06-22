namespace ApiCartobani.Domain.Fonctionnalites.Dtos;

using SharedKernel.Dtos;

public sealed class FonctionnaliteParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

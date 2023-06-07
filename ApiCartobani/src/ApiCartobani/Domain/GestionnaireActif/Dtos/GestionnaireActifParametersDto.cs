namespace ApiCartobani.Domain.GestionnaireActif.Dtos;

using SharedKernel.Dtos;

public sealed class GestionnaireActifParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

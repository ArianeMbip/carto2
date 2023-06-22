namespace ApiCartobani.Domain.InterfacesUtilisateur.Dtos;

using SharedKernel.Dtos;

public sealed class InterfaceUtilisateurParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

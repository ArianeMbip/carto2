namespace ApiCartobani.Domain.PiecesJointes.Dtos;

using SharedKernel.Dtos;

public sealed class PieceJointeParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

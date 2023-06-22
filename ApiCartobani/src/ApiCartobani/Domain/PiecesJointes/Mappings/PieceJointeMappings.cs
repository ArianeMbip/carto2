namespace ApiCartobani.Domain.PiecesJointes.Mappings;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes;
using Mapster;

public sealed class PieceJointeMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PieceJointe, PieceJointeDto>();
        config.NewConfig<PieceJointeForCreationDto, PieceJointe>()
            .TwoWays();
        config.NewConfig<PieceJointeForUpdateDto, PieceJointe>()
            .TwoWays();
    }
}
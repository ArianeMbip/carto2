namespace ApiCartobani.Domain.PiecesJointes.Features;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetPieceJointe
{
    public sealed class Query : IRequest<PieceJointeDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PieceJointeDto>
    {
        private readonly IPieceJointeRepository _pieceJointeRepository;
        private readonly IMapper _mapper;

        public Handler(IPieceJointeRepository pieceJointeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pieceJointeRepository = pieceJointeRepository;
        }

        public async Task<PieceJointeDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _pieceJointeRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<PieceJointeDto>(result);
        }
    }
}
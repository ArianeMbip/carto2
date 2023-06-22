namespace ApiCartobani.Domain.PiecesJointes.Features;

using ApiCartobani.Domain.PiecesJointes.Services;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddPieceJointe
{
    public sealed class Command : IRequest<PieceJointeDto>
    {
        public readonly PieceJointeForCreationDto PieceJointeToAdd;

        public Command(PieceJointeForCreationDto pieceJointeToAdd)
        {
            PieceJointeToAdd = pieceJointeToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, PieceJointeDto>
    {
        private readonly IPieceJointeRepository _pieceJointeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IPieceJointeRepository pieceJointeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _pieceJointeRepository = pieceJointeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PieceJointeDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var pieceJointe = PieceJointe.Create(request.PieceJointeToAdd);
            await _pieceJointeRepository.Add(pieceJointe, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var pieceJointeAdded = await _pieceJointeRepository.GetById(pieceJointe.Id, cancellationToken: cancellationToken);
            return _mapper.Map<PieceJointeDto>(pieceJointeAdded);
        }
    }
}
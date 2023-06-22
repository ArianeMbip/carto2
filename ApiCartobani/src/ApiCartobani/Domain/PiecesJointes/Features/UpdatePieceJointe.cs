namespace ApiCartobani.Domain.PiecesJointes.Features;

using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdatePieceJointe
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly PieceJointeForUpdateDto UpdatedPieceJointeData;

        public Command(Guid id, PieceJointeForUpdateDto updatedPieceJointeData)
        {
            Id = id;
            UpdatedPieceJointeData = updatedPieceJointeData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IPieceJointeRepository _pieceJointeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IPieceJointeRepository pieceJointeRepository, IUnitOfWork unitOfWork)
        {
            _pieceJointeRepository = pieceJointeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var pieceJointeToUpdate = await _pieceJointeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            pieceJointeToUpdate.Update(request.UpdatedPieceJointeData);
            _pieceJointeRepository.Update(pieceJointeToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
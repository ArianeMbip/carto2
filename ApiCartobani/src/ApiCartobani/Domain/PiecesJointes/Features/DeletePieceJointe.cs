namespace ApiCartobani.Domain.PiecesJointes.Features;

using ApiCartobani.Domain.PiecesJointes.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeletePieceJointe
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
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
            var recordToDelete = await _pieceJointeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _pieceJointeRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
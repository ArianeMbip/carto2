namespace ApiCartobani.Domain.Historiques.Features;

using ApiCartobani.Domain.Historiques.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteHistorique
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
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IHistoriqueRepository historiqueRepository, IUnitOfWork unitOfWork)
        {
            _historiqueRepository = historiqueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _historiqueRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _historiqueRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
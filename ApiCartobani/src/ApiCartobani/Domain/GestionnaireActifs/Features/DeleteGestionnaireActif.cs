namespace ApiCartobani.Domain.GestionnaireActifs.Features;

using ApiCartobani.Domain.GestionnaireActifs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteGestionnaireActif
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
        private readonly IGestionnaireActifRepository _gestionnaireActifRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IGestionnaireActifRepository gestionnaireActifRepository, IUnitOfWork unitOfWork)
        {
            _gestionnaireActifRepository = gestionnaireActifRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _gestionnaireActifRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _gestionnaireActifRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
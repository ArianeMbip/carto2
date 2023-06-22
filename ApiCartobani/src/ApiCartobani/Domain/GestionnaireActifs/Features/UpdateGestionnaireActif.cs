namespace ApiCartobani.Domain.GestionnaireActifs.Features;

using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateGestionnaireActif
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly GestionnaireActifForUpdateDto UpdatedGestionnaireActifData;

        public Command(Guid id, GestionnaireActifForUpdateDto updatedGestionnaireActifData)
        {
            Id = id;
            UpdatedGestionnaireActifData = updatedGestionnaireActifData;
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
            var gestionnaireActifToUpdate = await _gestionnaireActifRepository.GetById(request.Id, cancellationToken: cancellationToken);

            gestionnaireActifToUpdate.Update(request.UpdatedGestionnaireActifData);
            _gestionnaireActifRepository.Update(gestionnaireActifToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
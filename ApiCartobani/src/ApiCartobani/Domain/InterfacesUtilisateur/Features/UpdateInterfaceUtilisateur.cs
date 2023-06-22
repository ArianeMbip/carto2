namespace ApiCartobani.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateInterfaceUtilisateur
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly InterfaceUtilisateurForUpdateDto UpdatedInterfaceUtilisateurData;

        public Command(Guid id, InterfaceUtilisateurForUpdateDto updatedInterfaceUtilisateurData)
        {
            Id = id;
            UpdatedInterfaceUtilisateurData = updatedInterfaceUtilisateurData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IInterfaceUtilisateurRepository _interfaceUtilisateurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IInterfaceUtilisateurRepository interfaceUtilisateurRepository, IUnitOfWork unitOfWork)
        {
            _interfaceUtilisateurRepository = interfaceUtilisateurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var interfaceUtilisateurToUpdate = await _interfaceUtilisateurRepository.GetById(request.Id, cancellationToken: cancellationToken);

            interfaceUtilisateurToUpdate.Update(request.UpdatedInterfaceUtilisateurData);
            _interfaceUtilisateurRepository.Update(interfaceUtilisateurToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
namespace ApiCartobani.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteInterfaceUtilisateur
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
        private readonly IInterfaceUtilisateurRepository _interfaceUtilisateurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IInterfaceUtilisateurRepository interfaceUtilisateurRepository, IUnitOfWork unitOfWork)
        {
            _interfaceUtilisateurRepository = interfaceUtilisateurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _interfaceUtilisateurRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _interfaceUtilisateurRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
namespace ApiCartobani.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddInterfaceUtilisateur
{
    public sealed class Command : IRequest<InterfaceUtilisateurDto>
    {
        public readonly InterfaceUtilisateurForCreationDto InterfaceUtilisateurToAdd;

        public Command(InterfaceUtilisateurForCreationDto interfaceUtilisateurToAdd)
        {
            InterfaceUtilisateurToAdd = interfaceUtilisateurToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, InterfaceUtilisateurDto>
    {
        private readonly IInterfaceUtilisateurRepository _interfaceUtilisateurRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IInterfaceUtilisateurRepository interfaceUtilisateurRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _interfaceUtilisateurRepository = interfaceUtilisateurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InterfaceUtilisateurDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var interfaceUtilisateur = InterfaceUtilisateur.Create(request.InterfaceUtilisateurToAdd);
            await _interfaceUtilisateurRepository.Add(interfaceUtilisateur, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var interfaceUtilisateurAdded = await _interfaceUtilisateurRepository.GetById(interfaceUtilisateur.Id, cancellationToken: cancellationToken);
            return _mapper.Map<InterfaceUtilisateurDto>(interfaceUtilisateurAdded);
        }
    }
}
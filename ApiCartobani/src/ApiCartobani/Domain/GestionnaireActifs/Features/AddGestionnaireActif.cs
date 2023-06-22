namespace ApiCartobani.Domain.GestionnaireActifs.Features;

using ApiCartobani.Domain.GestionnaireActifs.Services;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddGestionnaireActif
{
    public sealed class Command : IRequest<GestionnaireActifDto>
    {
        public readonly GestionnaireActifForCreationDto GestionnaireActifToAdd;

        public Command(GestionnaireActifForCreationDto gestionnaireActifToAdd)
        {
            GestionnaireActifToAdd = gestionnaireActifToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, GestionnaireActifDto>
    {
        private readonly IGestionnaireActifRepository _gestionnaireActifRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IGestionnaireActifRepository gestionnaireActifRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _gestionnaireActifRepository = gestionnaireActifRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GestionnaireActifDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var gestionnaireActif = GestionnaireActif.Create(request.GestionnaireActifToAdd);
            await _gestionnaireActifRepository.Add(gestionnaireActif, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var gestionnaireActifAdded = await _gestionnaireActifRepository.GetById(gestionnaireActif.Id, cancellationToken: cancellationToken);
            return _mapper.Map<GestionnaireActifDto>(gestionnaireActifAdded);
        }
    }
}
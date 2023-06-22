namespace ApiCartobani.Domain.Fonctionnalites.Features;

using ApiCartobani.Domain.Fonctionnalites.Services;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddFonctionnalite
{
    public sealed class Command : IRequest<FonctionnaliteDto>
    {
        public readonly FonctionnaliteForCreationDto FonctionnaliteToAdd;

        public Command(FonctionnaliteForCreationDto fonctionnaliteToAdd)
        {
            FonctionnaliteToAdd = fonctionnaliteToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, FonctionnaliteDto>
    {
        private readonly IFonctionnaliteRepository _fonctionnaliteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IFonctionnaliteRepository fonctionnaliteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _fonctionnaliteRepository = fonctionnaliteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FonctionnaliteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var fonctionnalite = Fonctionnalite.Create(request.FonctionnaliteToAdd);
            await _fonctionnaliteRepository.Add(fonctionnalite, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var fonctionnaliteAdded = await _fonctionnaliteRepository.GetById(fonctionnalite.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FonctionnaliteDto>(fonctionnaliteAdded);
        }
    }
}
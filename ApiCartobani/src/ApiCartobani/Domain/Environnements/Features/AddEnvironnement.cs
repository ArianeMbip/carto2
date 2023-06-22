namespace ApiCartobani.Domain.Environnements.Features;

using ApiCartobani.Domain.Environnements.Services;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddEnvironnement
{
    public sealed class Command : IRequest<EnvironnementDto>
    {
        public readonly EnvironnementForCreationDto EnvironnementToAdd;

        public Command(EnvironnementForCreationDto environnementToAdd)
        {
            EnvironnementToAdd = environnementToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, EnvironnementDto>
    {
        private readonly IEnvironnementRepository _environnementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IEnvironnementRepository environnementRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _environnementRepository = environnementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnvironnementDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var environnement = Environnement.Create(request.EnvironnementToAdd);
            await _environnementRepository.Add(environnement, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var environnementAdded = await _environnementRepository.GetById(environnement.Id, cancellationToken: cancellationToken);
            return _mapper.Map<EnvironnementDto>(environnementAdded);
        }
    }
}
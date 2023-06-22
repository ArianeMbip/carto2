namespace ApiCartobani.Domain.Composants.Features;

using ApiCartobani.Domain.Composants.Services;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddComposant
{
    public sealed class Command : IRequest<ComposantDto>
    {
        public readonly ComposantForCreationDto ComposantToAdd;

        public Command(ComposantForCreationDto composantToAdd)
        {
            ComposantToAdd = composantToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ComposantDto>
    {
        private readonly IComposantRepository _composantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IComposantRepository composantRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _composantRepository = composantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ComposantDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var composant = Composant.Create(request.ComposantToAdd);
            await _composantRepository.Add(composant, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var composantAdded = await _composantRepository.GetById(composant.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ComposantDto>(composantAdded);
        }
    }
}
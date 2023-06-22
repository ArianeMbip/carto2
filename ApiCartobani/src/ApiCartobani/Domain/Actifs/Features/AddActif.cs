namespace ApiCartobani.Domain.Actifs.Features;

using ApiCartobani.Domain.Actifs.Services;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddActif
{
    public sealed class Command : IRequest<ActifDto>
    {
        public readonly ActifForCreationDto ActifToAdd;

        public Command(ActifForCreationDto actifToAdd)
        {
            ActifToAdd = actifToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ActifDto>
    {
        private readonly IActifRepository _actifRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IActifRepository actifRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _actifRepository = actifRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActifDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var actif = Actif.Create(request.ActifToAdd);
            await _actifRepository.Add(actif, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var actifAdded = await _actifRepository.GetById(actif.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ActifDto>(actifAdded);
        }
    }
}
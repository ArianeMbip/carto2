namespace ApiCartobani.Domain.Universs.Features;

using ApiCartobani.Domain.Universs.Services;
using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddUnivers
{
    public sealed class Command : IRequest<UniversDto>
    {
        public readonly UniversForCreationDto UniversToAdd;

        public Command(UniversForCreationDto universToAdd)
        {
            UniversToAdd = universToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, UniversDto>
    {
        private readonly IUniversRepository _universRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IUniversRepository universRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _universRepository = universRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UniversDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var univers = Univers.Create(request.UniversToAdd);
            await _universRepository.Add(univers, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var universAdded = await _universRepository.GetById(univers.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UniversDto>(universAdded);
        }
    }
}
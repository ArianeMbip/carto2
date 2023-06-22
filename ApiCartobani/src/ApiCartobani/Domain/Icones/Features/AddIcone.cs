namespace ApiCartobani.Domain.Icones.Features;

using ApiCartobani.Domain.Icones.Services;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddIcone
{
    public sealed class Command : IRequest<IconeDto>
    {
        public readonly IconeForCreationDto IconeToAdd;

        public Command(IconeForCreationDto iconeToAdd)
        {
            IconeToAdd = iconeToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, IconeDto>
    {
        private readonly IIconeRepository _iconeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IIconeRepository iconeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _iconeRepository = iconeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IconeDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var icone = Icone.Create(request.IconeToAdd);
            await _iconeRepository.Add(icone, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var iconeAdded = await _iconeRepository.GetById(icone.Id, cancellationToken: cancellationToken);
            return _mapper.Map<IconeDto>(iconeAdded);
        }
    }
}
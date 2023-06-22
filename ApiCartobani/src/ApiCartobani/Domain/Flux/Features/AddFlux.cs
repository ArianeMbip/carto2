namespace ApiCartobani.Domain.Flux.Features;

using ApiCartobani.Domain.Flux.Services;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddFlux
{
    public sealed class Command : IRequest<FluxDto>
    {
        public readonly FluxForCreationDto FluxToAdd;

        public Command(FluxForCreationDto fluxToAdd)
        {
            FluxToAdd = fluxToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, FluxDto>
    {
        private readonly IFluxRepository _fluxRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IFluxRepository fluxRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _fluxRepository = fluxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var flux = Flux.Create(request.FluxToAdd);
            await _fluxRepository.Add(flux, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var fluxAdded = await _fluxRepository.GetById(flux.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FluxDto>(fluxAdded);
        }
    }
}
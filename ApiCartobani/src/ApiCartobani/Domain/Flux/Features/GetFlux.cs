namespace ApiCartobani.Domain.Flux.Features;

using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetFlux
{
    public sealed class Query : IRequest<FluxDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, FluxDto>
    {
        private readonly IFluxRepository _fluxRepository;
        private readonly IMapper _mapper;

        public Handler(IFluxRepository fluxRepository, IMapper mapper)
        {
            _mapper = mapper;
            _fluxRepository = fluxRepository;
        }

        public async Task<FluxDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _fluxRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<FluxDto>(result);
        }
    }
}
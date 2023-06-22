namespace ApiCartobani.Domain.Environnements.Features;

using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetEnvironnement
{
    public sealed class Query : IRequest<EnvironnementDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, EnvironnementDto>
    {
        private readonly IEnvironnementRepository _environnementRepository;
        private readonly IMapper _mapper;

        public Handler(IEnvironnementRepository environnementRepository, IMapper mapper)
        {
            _mapper = mapper;
            _environnementRepository = environnementRepository;
        }

        public async Task<EnvironnementDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _environnementRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<EnvironnementDto>(result);
        }
    }
}
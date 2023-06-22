namespace ApiCartobani.Domain.Actifs.Features;

using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetActif
{
    public sealed class Query : IRequest<ActifDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ActifDto>
    {
        private readonly IActifRepository _actifRepository;
        private readonly IMapper _mapper;

        public Handler(IActifRepository actifRepository, IMapper mapper)
        {
            _mapper = mapper;
            _actifRepository = actifRepository;
        }

        public async Task<ActifDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _actifRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ActifDto>(result);
        }
    }
}
namespace ApiCartobani.Domain.Composants.Features;

using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetComposant
{
    public sealed class Query : IRequest<ComposantDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ComposantDto>
    {
        private readonly IComposantRepository _composantRepository;
        private readonly IMapper _mapper;

        public Handler(IComposantRepository composantRepository, IMapper mapper)
        {
            _mapper = mapper;
            _composantRepository = composantRepository;
        }

        public async Task<ComposantDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _composantRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ComposantDto>(result);
        }
    }
}
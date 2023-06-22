namespace ApiCartobani.Domain.Attributs.Features;

using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetAttribut
{
    public sealed class Query : IRequest<AttributDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, AttributDto>
    {
        private readonly IAttributRepository _attributRepository;
        private readonly IMapper _mapper;

        public Handler(IAttributRepository attributRepository, IMapper mapper)
        {
            _mapper = mapper;
            _attributRepository = attributRepository;
        }

        public async Task<AttributDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _attributRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<AttributDto>(result);
        }
    }
}
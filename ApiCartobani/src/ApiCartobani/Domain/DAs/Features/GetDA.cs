namespace ApiCartobani.Domain.DAs.Features;

using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetDA
{
    public sealed class Query : IRequest<DADto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, DADto>
    {
        private readonly IDARepository _dARepository;
        private readonly IMapper _mapper;

        public Handler(IDARepository dARepository, IMapper mapper)
        {
            _mapper = mapper;
            _dARepository = dARepository;
        }

        public async Task<DADto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _dARepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<DADto>(result);
        }
    }
}
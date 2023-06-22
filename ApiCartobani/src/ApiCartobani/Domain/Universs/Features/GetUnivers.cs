namespace ApiCartobani.Domain.Universs.Features;

using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Domain.Universs.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetUnivers
{
    public sealed class Query : IRequest<UniversDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, UniversDto>
    {
        private readonly IUniversRepository _universRepository;
        private readonly IMapper _mapper;

        public Handler(IUniversRepository universRepository, IMapper mapper)
        {
            _mapper = mapper;
            _universRepository = universRepository;
        }

        public async Task<UniversDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _universRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<UniversDto>(result);
        }
    }
}
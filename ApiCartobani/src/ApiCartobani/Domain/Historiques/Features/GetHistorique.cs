namespace ApiCartobani.Domain.Historiques.Features;

using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetHistorique
{
    public sealed class Query : IRequest<HistoriqueDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, HistoriqueDto>
    {
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly IMapper _mapper;

        public Handler(IHistoriqueRepository historiqueRepository, IMapper mapper)
        {
            _mapper = mapper;
            _historiqueRepository = historiqueRepository;
        }

        public async Task<HistoriqueDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _historiqueRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<HistoriqueDto>(result);
        }
    }
}
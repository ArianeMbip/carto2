namespace ApiCartobani.Domain.Historiques.Features;

using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.Domain.Historiques.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetHistoriqueList
{
    public sealed class Query : IRequest<PagedList<HistoriqueDto>>
    {
        public readonly HistoriqueParametersDto QueryParameters;

        public Query(HistoriqueParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<HistoriqueDto>>
    {
        private readonly IHistoriqueRepository _historiqueRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IHistoriqueRepository historiqueRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _historiqueRepository = historiqueRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<HistoriqueDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _historiqueRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<HistoriqueDto>();

            return await PagedList<HistoriqueDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
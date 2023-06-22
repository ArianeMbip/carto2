namespace ApiCartobani.Domain.Actifs.Features;

using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.Domain.Actifs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetActifList
{
    public sealed class Query : IRequest<PagedList<ActifDto>>
    {
        public readonly ActifParametersDto QueryParameters;

        public Query(ActifParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ActifDto>>
    {
        private readonly IActifRepository _actifRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IActifRepository actifRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _actifRepository = actifRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ActifDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            //var collection = _actifRepository.Query().AsNoTracking().Include((p) => p.ValeurAttributs);
            var collection = _actifRepository.Query().AsNoTracking();
            //var collection = _actifRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ActifDto>();

            return await PagedList<ActifDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
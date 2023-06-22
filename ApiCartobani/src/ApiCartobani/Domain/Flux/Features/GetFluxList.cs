namespace ApiCartobani.Domain.Flux.Features;

using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.Domain.Flux.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetFluxList
{
    public sealed class Query : IRequest<PagedList<FluxDto>>
    {
        public readonly FluxParametersDto QueryParameters;

        public Query(FluxParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<FluxDto>>
    {
        private readonly IFluxRepository _fluxRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IFluxRepository fluxRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _fluxRepository = fluxRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<FluxDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _fluxRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<FluxDto>();

            return await PagedList<FluxDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
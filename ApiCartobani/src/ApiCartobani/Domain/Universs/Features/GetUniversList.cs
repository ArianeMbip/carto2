namespace ApiCartobani.Domain.Universs.Features;

using ApiCartobani.Domain.Universs.Dtos;
using ApiCartobani.Domain.Universs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUniversList
{
    public sealed class Query : IRequest<PagedList<UniversDto>>
    {
        public readonly UniversParametersDto QueryParameters;

        public Query(UniversParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<UniversDto>>
    {
        private readonly IUniversRepository _universRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IUniversRepository universRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _universRepository = universRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UniversDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _universRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<UniversDto>();

            return await PagedList<UniversDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
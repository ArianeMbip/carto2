namespace ApiCartobani.Domain.Attributs.Features;

using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.Domain.Attributs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetAttributList
{
    public sealed class Query : IRequest<PagedList<AttributDto>>
    {
        public readonly AttributParametersDto QueryParameters;

        public Query(AttributParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<AttributDto>>
    {
        private readonly IAttributRepository _attributRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IAttributRepository attributRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _attributRepository = attributRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<AttributDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _attributRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<AttributDto>();

            return await PagedList<AttributDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
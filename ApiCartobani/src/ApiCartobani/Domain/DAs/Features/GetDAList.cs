namespace ApiCartobani.Domain.DAs.Features;

using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.Domain.DAs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetDAList
{
    public sealed class Query : IRequest<PagedList<DADto>>
    {
        public readonly DAParametersDto QueryParameters;

        public Query(DAParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<DADto>>
    {
        private readonly IDARepository _dARepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IDARepository dARepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _dARepository = dARepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<DADto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _dARepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<DADto>();

            return await PagedList<DADto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
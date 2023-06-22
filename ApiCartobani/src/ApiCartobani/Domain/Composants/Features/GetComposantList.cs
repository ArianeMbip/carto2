namespace ApiCartobani.Domain.Composants.Features;

using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.Domain.Composants.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetComposantList
{
    public sealed class Query : IRequest<PagedList<ComposantDto>>
    {
        public readonly ComposantParametersDto QueryParameters;

        public Query(ComposantParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ComposantDto>>
    {
        private readonly IComposantRepository _composantRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IComposantRepository composantRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _composantRepository = composantRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ComposantDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _composantRepository.Query().AsNoTracking();
            

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            //var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = collection
                .ProjectToType<ComposantDto>();

            return await PagedList<ComposantDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
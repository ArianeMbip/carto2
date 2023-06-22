namespace ApiCartobani.Domain.Environnements.Features;

using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.Domain.Environnements.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetEnvironnementList
{
    public sealed class Query : IRequest<PagedList<EnvironnementDto>>
    {
        public readonly EnvironnementParametersDto QueryParameters;

        public Query(EnvironnementParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<EnvironnementDto>>
    {
        private readonly IEnvironnementRepository _environnementRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IEnvironnementRepository environnementRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _environnementRepository = environnementRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<EnvironnementDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _environnementRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<EnvironnementDto>();

            return await PagedList<EnvironnementDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
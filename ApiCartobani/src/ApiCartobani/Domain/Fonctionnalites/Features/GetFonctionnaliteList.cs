namespace ApiCartobani.Domain.Fonctionnalites.Features;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.Domain.Fonctionnalites.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetFonctionnaliteList
{
    public sealed class Query : IRequest<PagedList<FonctionnaliteDto>>
    {
        public readonly FonctionnaliteParametersDto QueryParameters;

        public Query(FonctionnaliteParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<FonctionnaliteDto>>
    {
        private readonly IFonctionnaliteRepository _fonctionnaliteRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IFonctionnaliteRepository fonctionnaliteRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _fonctionnaliteRepository = fonctionnaliteRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<FonctionnaliteDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _fonctionnaliteRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<FonctionnaliteDto>();

            return await PagedList<FonctionnaliteDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
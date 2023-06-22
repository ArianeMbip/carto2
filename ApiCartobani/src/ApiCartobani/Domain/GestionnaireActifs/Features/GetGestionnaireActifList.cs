namespace ApiCartobani.Domain.GestionnaireActifs.Features;

using ApiCartobani.Domain.GestionnaireActifs.Dtos;
using ApiCartobani.Domain.GestionnaireActifs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetGestionnaireActifList
{
    public sealed class Query : IRequest<PagedList<GestionnaireActifDto>>
    {
        public readonly GestionnaireActifParametersDto QueryParameters;

        public Query(GestionnaireActifParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<GestionnaireActifDto>>
    {
        private readonly IGestionnaireActifRepository _gestionnaireActifRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IGestionnaireActifRepository gestionnaireActifRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _gestionnaireActifRepository = gestionnaireActifRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<GestionnaireActifDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            //var collection = _gestionnaireActifRepository.Query().AsNoTracking();
            var collection = _gestionnaireActifRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<GestionnaireActifDto>();

            return await PagedList<GestionnaireActifDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
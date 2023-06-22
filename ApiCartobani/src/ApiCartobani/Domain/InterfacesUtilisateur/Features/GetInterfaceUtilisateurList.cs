namespace ApiCartobani.Domain.InterfacesUtilisateur.Features;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Domain.InterfacesUtilisateur.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetInterfaceUtilisateurList
{
    public sealed class Query : IRequest<PagedList<InterfaceUtilisateurDto>>
    {
        public readonly InterfaceUtilisateurParametersDto QueryParameters;

        public Query(InterfaceUtilisateurParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<InterfaceUtilisateurDto>>
    {
        private readonly IInterfaceUtilisateurRepository _interfaceUtilisateurRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IInterfaceUtilisateurRepository interfaceUtilisateurRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _interfaceUtilisateurRepository = interfaceUtilisateurRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<InterfaceUtilisateurDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _interfaceUtilisateurRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<InterfaceUtilisateurDto>();

            return await PagedList<InterfaceUtilisateurDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
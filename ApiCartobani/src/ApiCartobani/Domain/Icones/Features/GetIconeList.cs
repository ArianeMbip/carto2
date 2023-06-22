namespace ApiCartobani.Domain.Icones.Features;

using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Domain.Icones.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetIconeList
{
    public sealed class Query : IRequest<PagedList<IconeDto>>
    {
        public readonly IconeParametersDto QueryParameters;

        public Query(IconeParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<IconeDto>>
    {
        private readonly IIconeRepository _iconeRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IIconeRepository iconeRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _iconeRepository = iconeRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<IconeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _iconeRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<IconeDto>();

            return await PagedList<IconeDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
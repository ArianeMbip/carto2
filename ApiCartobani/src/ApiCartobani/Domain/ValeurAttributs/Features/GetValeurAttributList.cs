namespace ApiCartobani.Domain.ValeurAttributs.Features;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Domain.ValeurAttributs.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetValeurAttributList
{
    public sealed class Query : IRequest<PagedList<ValeurAttributDto>>
    {
        public readonly ValeurAttributParametersDto QueryParameters;

        public Query(ValeurAttributParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ValeurAttributDto>>
    {
        private readonly IValeurAttributRepository _valeurAttributRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IValeurAttributRepository valeurAttributRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _valeurAttributRepository = valeurAttributRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ValeurAttributDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _valeurAttributRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ValeurAttributDto>();

            return await PagedList<ValeurAttributDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
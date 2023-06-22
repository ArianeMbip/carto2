namespace ApiCartobani.Domain.PiecesJointes.Features;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Domain.PiecesJointes.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetPieceJointeList
{
    public sealed class Query : IRequest<PagedList<PieceJointeDto>>
    {
        public readonly PieceJointeParametersDto QueryParameters;

        public Query(PieceJointeParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<PieceJointeDto>>
    {
        private readonly IPieceJointeRepository _pieceJointeRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IPieceJointeRepository pieceJointeRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _pieceJointeRepository = pieceJointeRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<PieceJointeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _pieceJointeRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<PieceJointeDto>();

            return await PagedList<PieceJointeDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
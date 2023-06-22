namespace ApiCartobani.Domain.RolePrivileges.Features;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetRolePrivilegeList
{
    public sealed class Query : IRequest<PagedList<RolePrivilegeDto>>
    {
        public readonly RolePrivilegeParametersDto QueryParameters;

        public Query(RolePrivilegeParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<RolePrivilegeDto>>
    {
        private readonly IRolePrivilegeRepository _rolePrivilegeRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IRolePrivilegeRepository rolePrivilegeRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<RolePrivilegeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _rolePrivilegeRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<RolePrivilegeDto>();

            return await PagedList<RolePrivilegeDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
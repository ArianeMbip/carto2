namespace ApiCartobani.Domain.Contacts.Features;

using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts.Services;
using ApiCartobani.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetContactList
{
    public sealed class Query : IRequest<PagedList<ContactDto>>
    {
        public readonly ContactParametersDto QueryParameters;

        public Query(ContactParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<ContactDto>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IContactRepository contactRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<ContactDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _contactRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<ContactDto>();

            return await PagedList<ContactDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
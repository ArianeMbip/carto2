namespace ApiCartobani.Domain.Contacts.Features;

using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetContact
{
    public sealed class Query : IRequest<ContactDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ContactDto>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public Handler(IContactRepository contactRepository, IMapper mapper)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<ContactDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _contactRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ContactDto>(result);
        }
    }
}
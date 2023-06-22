namespace ApiCartobani.Domain.Contacts.Features;

using ApiCartobani.Domain.Contacts.Services;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddContact
{
    public sealed class Command : IRequest<ContactDto>
    {
        public readonly ContactForCreationDto ContactToAdd;

        public Command(ContactForCreationDto contactToAdd)
        {
            ContactToAdd = contactToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ContactDto>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IContactRepository contactRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var contact = Contact.Create(request.ContactToAdd);
            await _contactRepository.Add(contact, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var contactAdded = await _contactRepository.GetById(contact.Id, cancellationToken: cancellationToken);
            return _mapper.Map<ContactDto>(contactAdded);
        }
    }
}